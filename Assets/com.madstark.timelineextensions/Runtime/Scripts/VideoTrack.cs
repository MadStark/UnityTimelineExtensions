using MadStark.Timeline.Mesmerise;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace MadStark.Timeline
{
    [TrackColor(0.06666666667f, 0.3333333333f, 0.7490196078f)]
    [TrackClipType(typeof(VideoPlayableAsset))]
    [TrackBindingType(typeof(UnityEngine.Video.VideoPlayer))]
    public class VideoTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<VideoPlayableMixer>.Create(graph, inputCount);
        }
    }
}