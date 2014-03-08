using Microsoft.Kinect;

public class KinectData
{
    // Make a global variable for the kinect sensor so that all parts of the program can use it
    public static KinectSensor kinect = null;

    public static Skeleton skeletonData;

    private int leftHandY;
    private int rightHandY;
    private int headY;
    private int hipCentreY;

    public KinectData()
    {
        skeletonData = new Skeleton[];
        kinect = null;
        leftHandY = 0;
        rightHandY = 0;
        headY = 0;
        hipCentreY = 0;
    }

    public void StartKinect()
    {
        kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected); // Get first Kinect Sensor
        kinect.SkeletonStream.Enable(); // Enable skeletal tracking

        skeletonData = new Skeleton[kinect.SkeletonStream.FrameSkeletonArrayLength]; // Allocate ST data

        kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady); // Get Ready for Skeleton Ready Events

        kinect.Start(); // Start Kinect sensor
    }

    public static void StopKinect() // Stops the kinect sensor
    {
        if (kinect != null)
        {
            kinect.Stop();
            kinect.AudioSource.Stop();
        }
    }

    public void UpdateSkeleton(object sender, SkeletonFrameReadyEventArgs e)
    {
        using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame()) // Open the Skeleton frame
        {
            if (skeletonFrame != null && this.skeletonData != null) // check that a frame is available
            {
                skeletonFrame.CopySkeletonDataTo(this.skeletonData); // get the skeletal information in this frame
            
                leftHandY = skeletonData.Joints[JointType.HandLeft].Position.Y;
                rightHandY = skeletonData.Joints[JointType.HandRight].Position.Y;
                headY = skeletonData.Joints[JointType.Head].Position.Y;
                hipCentreY = skeletonData.Joints[JointType.HipCenter].Position.Y;
            }
        }
    }

    public static int GetLeftHandY()
    {
        return leftHandY;
    }

    public static int GetRightHandY()
    {
        return rightHandY;
    }

    public static int GetHeadY()
    {
        return headY;
    }

    public static int GetHipY()
    {
        return hipCentreY;
    }

    
}