using UnityEngine;

namespace Emotions
{
    public enum Emotion {None = 0, Anger = 1, Fear = 2, Sadness = 3, Peace = 4, Joy = 5, Love = 6}

    public static class EmotionExtensions
    {
        public static Color GetColor(this Emotion emotion) => emotion switch {
            Emotion.Anger => new Color(1, 29f/255f, 0, 1f),
            Emotion.Fear => new Color(99f/255f, 14f/255f, 144f/255f, 1f),
            Emotion.Sadness => new Color(0, 133f/255f, 202f/255f, 1f),
            Emotion.Peace => Color.cyan,
            Emotion.Joy => Color.yellow,
            Emotion.Love => Color.magenta,
            _ => Color.white
        };

        public static float GetSpeed(this Emotion emotion) => emotion switch {
            Emotion.Anger => 40f,
            Emotion.Fear => 60f,
            Emotion.Sadness => 20f,
            Emotion.Peace => -20f,
            Emotion.Joy => -60f,
            Emotion.Love => -40f,
            _ => 0f
        };

        public static Emotion GetRandomEmotion() => (Emotion)Random.Range(1, 4);
        public static bool IsPositive(this Emotion emotion) => emotion >= Emotion.Peace;
    }
}