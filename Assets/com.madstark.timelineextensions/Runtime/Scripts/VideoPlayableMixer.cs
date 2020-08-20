using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace MadStark.Timeline
{
    namespace Mesmerise
{
    public class VideoPlayableMixer : PlayableBehaviour
    {
        private VideoClip defaultClip;
        private bool defaulPlayingState;

        private VideoPlayer videoPlayer;
        private bool firstFrameHappened;

        private VideoPlayableBehaviour current;
        

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);

            if (!Application.isPlaying)
                return;

            videoPlayer = playerData as VideoPlayer;
            if (videoPlayer == null)
                return;

            if (!firstFrameHappened)
            {
                defaultClip = videoPlayer.clip;
                defaulPlayingState = videoPlayer.isPlaying;
                firstFrameHappened = true;
            }

            ScriptPlayable<VideoPlayableBehaviour> inputPlayable = default;

            int inputCount = playable.GetInputCount();
            if (inputCount == 0)
                return;
            float maxWeight = 0;
            for (int i = 0; i < inputCount; i++)
            {
                if (playable.GetInputWeight(i) > maxWeight)
                    inputPlayable = (ScriptPlayable<VideoPlayableBehaviour>)playable.GetInput(i);
            }
            VideoPlayableBehaviour input = inputPlayable.GetBehaviour();
            if (current != input)
            {
                current = input;
                if (input != null)
                {
                    videoPlayer.clip = input.videoClip;
                    videoPlayer.time = 0;
                    videoPlayer.Play();
                }
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            base.OnPlayableDestroy(playable);

            firstFrameHappened = false;

            if (videoPlayer != null)
            {
                videoPlayer.clip = defaultClip;
                if (defaulPlayingState)
                    videoPlayer.Play();
                else
                    videoPlayer.Stop();
            }
        }
    }
}
}