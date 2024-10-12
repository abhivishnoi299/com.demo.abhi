namespace AKRGS.Framework
{
    public enum EStates : byte
    {
        ARPlacement = 0,
        ARView = 1, //To set view mode for AR and pause model placement.
        ThreeDView = 2,
        ZoomInOut = 3,
        Rotation = 4,
        Reposition = 5,
        ObjectManipulation = 6 //Use to set ZoomInOut, Rotation and Reposition states together.
    }
}
