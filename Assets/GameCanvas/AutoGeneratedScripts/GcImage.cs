/*------------------------------------------------------------*/
// <summary>GameCanvas for Unity</summary>
// <author>Seibe TAKAHASHI</author>
// <remarks>
// (c) 2015-2020 Smart Device Programming.
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
// </remarks>
/*------------------------------------------------------------*/
namespace GameCanvas
{
    public readonly partial struct GcImage : System.IEquatable<GcImage>
    {
        internal const int __Length__ = 7;
        public static readonly GcImage BallRed = new GcImage("BallRed", 24, 24);
        public static readonly GcImage BallYellow = new GcImage("BallYellow", 24, 24);
        public static readonly GcImage BlueSky = new GcImage("BlueSky", 640, 480);
        public static readonly GcImage DoneIcon = new GcImage("DoneIcon", 640, 640);
        public static readonly GcImage Example1 = new GcImage("Example1", 304, 304);
        public static readonly GcImage Example2 = new GcImage("Example2", 304, 304);
        public static readonly GcImage LightsOutLogo = new GcImage("LightsOutLogo", 640, 640);
    }
}
