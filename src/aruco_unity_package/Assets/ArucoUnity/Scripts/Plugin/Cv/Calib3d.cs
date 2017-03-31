﻿using System.Runtime.InteropServices;

namespace ArucoUnity
{
  /// \addtogroup aruco_unity_package
  /// \{

  namespace Plugin
  {
    public static partial class Cv
    {
      public static partial class Calib3d
      {
        // Enums

        public enum Calib
        {
          UseIntrinsicGuess = 0x00001,
          FixAspectRatio = 0x00002,
          FixPrincipalPoint = 0x00004,
          ZeroTangentDist = 0x00008,
          FixFocalLength = 0x00010,
          FixK1 = 0x00020,
          FixK2 = 0x00040,
          FixK3 = 0x00080,
          FixK4 = 0x00800,
          FixK5 = 0x01000,
          FixK6 = 0x02000,
          RationalModel = 0x04000,
          ThinPrismModel = 0x08000,
          FixS1S2S3S4 = 0x10000,
          TiltedModel = 0x40000,
          FixTauxTauy = 0x80000,
          // only for stereo
          FixIntrinsic = 0x00100,
          SameFocalLength = 0x00200,
          // for stereo rectification
          ZeroDisparity = 0x00400,
          UseLu = (1 << 17)
        };

        public enum StereoRectifyFlags
        {
          ZeroDisparity = 1024
        };

        // Native functions

        [DllImport("ArucoUnity")]
        static extern double au_cv_calib3d_calibrateCamera1(System.IntPtr objectPoints, System.IntPtr imagePoints, System.IntPtr imageSize,
          System.IntPtr cameraMatrix, System.IntPtr distCoeffs, out System.IntPtr rvecs, out System.IntPtr tvecs,
          System.IntPtr stdDeviationsIntrinsics, System.IntPtr stdDeviationsExtrinsics, System.IntPtr perViewErrors, int flags,
          System.IntPtr criteria, System.IntPtr exception);

        [DllImport("ArucoUnity")]
        static extern double au_cv_calib3d_calibrateCamera2(System.IntPtr objectPoints, System.IntPtr imagePoints, System.IntPtr imageSize,
          System.IntPtr cameraMatrix, System.IntPtr distCoeffs, out System.IntPtr rvecs, out System.IntPtr tvecs, int flags, System.IntPtr criteria,
          System.IntPtr exception);

        [DllImport("ArucoUnity")]
        static extern System.IntPtr au_cv_calib3d_initCameraMatrix2D(System.IntPtr objectPoints, System.IntPtr imagePoints, System.IntPtr imageSize,
          double aspectRatio, System.IntPtr exception);

        [DllImport("ArucoUnity")]
        static extern void au_cv_calib3d_Rodrigues(System.IntPtr rotationVector, out System.IntPtr rotationMatrix, System.IntPtr exception);

        [DllImport("ArucoUnity")]
        static extern double au_cv_calib3d_stereoCalibrate(System.IntPtr objectPoints, System.IntPtr imagePoints1, System.IntPtr imagePoints2,
          System.IntPtr cameraMatrix1, System.IntPtr distCoeffs1, System.IntPtr cameraMatrix2, System.IntPtr distCoeffs2, System.IntPtr imageSize,
          out System.IntPtr R, out System.IntPtr T, out System.IntPtr E, out System.IntPtr F, int flags, System.IntPtr criteria,
          System.IntPtr exception);

        [DllImport("ArucoUnity")]
        static extern void au_cv_calib3d_stereoRectify(System.IntPtr cameraMatrix1, System.IntPtr distCoeffs1, System.IntPtr cameraMatrix2,
          System.IntPtr distCoeffs2, System.IntPtr imageSize, System.IntPtr R, System.IntPtr T, out System.IntPtr R1, out System.IntPtr R2,
          out System.IntPtr P1, out System.IntPtr P2, out System.IntPtr Q, int flags, double alpha, System.IntPtr newImageSize,
          System.IntPtr validPixROI1, System.IntPtr validPixROI2, System.IntPtr exception);

        // Static functions

        public static double CalibrateCamera(Std.VectorVectorPoint3f objectPoints, Std.VectorVectorPoint2f imagePoints, Core.Size imageSize,
          Core.Mat cameraMatrix, Core.Mat distCoeffs, out Std.VectorMat rvecs, out Std.VectorMat tvecs, Std.VectorDouble stdDeviationsIntrinsics,
          Std.VectorDouble stdDeviationsExtrinsics, Std.VectorDouble perViewErrors, Calib flags, Core.TermCriteria criteria = null)
        {
          criteria = (criteria != null) ? criteria : new Core.TermCriteria(Core.TermCriteria.Type.Count | Core.TermCriteria.Type.Eps, 30, Core.EPSILON);
          Core.Exception exception = new Core.Exception();
          System.IntPtr rvecsPtr, tvecsPtr;

          double error = au_cv_calib3d_calibrateCamera1(objectPoints.cppPtr, imagePoints.cppPtr, imageSize.cppPtr, cameraMatrix.cppPtr,
            distCoeffs.cppPtr, out rvecsPtr, out tvecsPtr, stdDeviationsIntrinsics.cppPtr, stdDeviationsExtrinsics.cppPtr, perViewErrors.cppPtr,
            (int)flags, criteria.cppPtr, exception.cppPtr);
          rvecs = new Std.VectorMat(rvecsPtr);
          tvecs = new Std.VectorMat(tvecsPtr);
          exception.Check();

          return error;
        }

        public static double CalibrateCamera(Std.VectorVectorPoint3f objectPoints, Std.VectorVectorPoint2f imagePoints, Core.Size imageSize,
          Core.Mat cameraMatrix, Core.Mat distCoeffs, out Std.VectorMat rvecs, out Std.VectorMat tvecs, Calib flags,
          Core.TermCriteria criteria = null)
        {
          criteria = (criteria != null) ? criteria : new Core.TermCriteria(Core.TermCriteria.Type.Count | Core.TermCriteria.Type.Eps, 30, Core.EPSILON);
          Core.Exception exception = new Core.Exception();
          System.IntPtr rvecsPtr, tvecsPtr;

          double error = au_cv_calib3d_calibrateCamera2(objectPoints.cppPtr, imagePoints.cppPtr, imageSize.cppPtr, cameraMatrix.cppPtr,
            distCoeffs.cppPtr, out rvecsPtr, out tvecsPtr, (int)flags, criteria.cppPtr, exception.cppPtr);
          rvecs = new Std.VectorMat(rvecsPtr);
          tvecs = new Std.VectorMat(tvecsPtr);
          exception.Check();

          return error;
        }

        public static Core.Mat InitCameraMatrix2D(Std.VectorVectorPoint3f objectPoints, Std.VectorVectorPoint2f imagePoints, Core.Size imageSize,
          double aspectRatio = 1.0)
        {
          Core.Exception exception = new Core.Exception();
          System.IntPtr cameraMatrixPtr = au_cv_calib3d_initCameraMatrix2D(objectPoints.cppPtr, imagePoints.cppPtr, imageSize.cppPtr, aspectRatio,
            exception.cppPtr);
          exception.Check();
          return new Core.Mat(cameraMatrixPtr);
        }

        public static void Rodrigues(Core.Vec3d rotationVector, out Core.Mat rotationMatrix)
        {
          Core.Exception exception = new Core.Exception();
          System.IntPtr rotationMatPtr;
          au_cv_calib3d_Rodrigues(rotationVector.cppPtr, out rotationMatPtr, exception.cppPtr);
          rotationMatrix = new Core.Mat(rotationMatPtr);
          exception.Check();
        }

        public static double StereoCalibrate(Std.VectorVectorPoint3f objectPoints, Std.VectorVectorPoint2f imagePoints1,
          Std.VectorVectorPoint2f imagePoints2, Core.Mat cameraMatrix1, Core.Mat distCoeffs1, Core.Mat cameraMatrix2, Core.Mat distCoeffs2,
          Core.Size imageSize, out Core.Mat rvec, out Core.Mat tvec, out Core.Mat E, out Core.Mat F, Calib flags = Calib.FixIntrinsic,
          Core.TermCriteria criteria = null)
        {
          criteria = (criteria != null) ? criteria : new Core.TermCriteria(Core.TermCriteria.Type.Count | Core.TermCriteria.Type.Eps, 30, 1e-6);
          Core.Exception exception = new Core.Exception();
          System.IntPtr rvecPtr, tvecPtr, EPtr, FPtr;

          double error = au_cv_calib3d_stereoCalibrate(objectPoints.cppPtr, imagePoints1.cppPtr, imagePoints2.cppPtr, cameraMatrix1.cppPtr,
            distCoeffs1.cppPtr, cameraMatrix2.cppPtr, distCoeffs2.cppPtr, imageSize.cppPtr, out rvecPtr, out tvecPtr, out EPtr, out FPtr, (int)flags,
            criteria.cppPtr, exception.cppPtr);
          rvec = new Core.Mat(rvecPtr);
          tvec = new Core.Mat(tvecPtr);
          E = new Core.Mat(EPtr);
          F = new Core.Mat(FPtr);
          exception.Check();

          return error;
        }

        public static void StereoRectify(Core.Mat cameraMatrix1, Core.Mat distCoeffs1, Core.Mat cameraMatrix2, Core.Mat distCoeffs2,
          Core.Size imageSize, Core.Mat rvec, Core.Mat tvec, out Core.Mat R1, out Core.Mat R2, out Core.Mat P1, out Core.Mat P2, out Core.Mat Q,
          StereoRectifyFlags flags = StereoRectifyFlags.ZeroDisparity, double alpha = -1, Core.Size newImageSize = null,
          Core.Rect validPixROI1 = null, Core.Rect validPixROI2 = null)
        {
          newImageSize = (newImageSize != null) ? newImageSize : new Core.Size();
          System.IntPtr validPixROI1Ptr = (validPixROI1 != null) ? validPixROI1.cppPtr : System.IntPtr.Zero;
          System.IntPtr validPixROI2Ptr = (validPixROI2 != null) ? validPixROI2.cppPtr : System.IntPtr.Zero;
          Core.Exception exception = new Core.Exception();
          System.IntPtr R1Ptr, R2Ptr, P1Ptr, P2Ptr, QPtr;

          au_cv_calib3d_stereoRectify(cameraMatrix1.cppPtr, distCoeffs1.cppPtr, cameraMatrix2.cppPtr, distCoeffs2.cppPtr, imageSize.cppPtr,
            rvec.cppPtr, tvec.cppPtr, out R1Ptr, out R2Ptr, out P1Ptr, out P2Ptr, out QPtr, (int)flags, alpha, newImageSize.cppPtr, validPixROI2.cppPtr,
            validPixROI2.cppPtr, exception.cppPtr);
          R1 = new Core.Mat(R1Ptr);
          R2 = new Core.Mat(R2Ptr);
          P1 = new Core.Mat(P1Ptr);
          P2 = new Core.Mat(P2Ptr);
          Q = new Core.Mat(QPtr);

          exception.Check();
        }
      }
    }
  }

  /// \} aruco_unity_package
}