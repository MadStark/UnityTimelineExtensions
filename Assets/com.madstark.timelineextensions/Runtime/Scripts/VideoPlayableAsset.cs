using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Video;

namespace MadStark.Timeline
{
    public class VideoPlayableAsset : PlayableAsset, ITimelineClipAsset
    {
        public ClipCaps clipCaps => ClipCaps.Blending;

        public VideoClip videoClip;
        

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<VideoPlayableBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.videoClip = videoClip;
            return playable;
        }
    }
}