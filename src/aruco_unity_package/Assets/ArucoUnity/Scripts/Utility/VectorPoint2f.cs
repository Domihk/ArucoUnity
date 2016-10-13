using System.Runtime.InteropServices;

namespace ArucoUnity
{
  namespace Utility
  {
    public class VectorPoint2f : HandleCvPtr
    {
      // Constructor & Destructor
      [DllImport("ArucoUnity")]
      static extern System.IntPtr au_vectorPoint2f_new();

      [DllImport("ArucoUnity")]
      static extern void au_vectorPoint2f_delete(System.IntPtr vector);

      // Functions
      [DllImport("ArucoUnity")]
      static extern System.IntPtr au_vectorPoint2f_at(System.IntPtr vector, int pos, System.IntPtr exception);

      [DllImport("ArucoUnity")]
      static extern unsafe System.IntPtr* au_vectorPoint2f_data(System.IntPtr vector);

      [DllImport("ArucoUnity")]
      static extern void au_vectorPoint2f_push_back(System.IntPtr vector, System.IntPtr value);

      [DllImport("ArucoUnity")]
      static extern int au_vectorPoint2f_size(System.IntPtr vector);

      public VectorPoint2f() : base(au_vectorPoint2f_new())
      {
      }

      public VectorPoint2f(System.IntPtr vectorPoint2fPtr, DeleteResponsibility deleteResponsibility = DeleteResponsibility.True) 
        : base(vectorPoint2fPtr, deleteResponsibility)
      {
      }

      protected override void DeleteCvPtr()
      {
        au_vectorPoint2f_delete(cvPtr);
      }

      public Point2f At(int pos) 
      {
        Exception exception = new Exception();
        Point2f element = new Point2f(au_vectorPoint2f_at(cvPtr, pos, exception.cvPtr), DeleteResponsibility.False);
        exception.Check();
        return element;
      }

      public unsafe Point2f[] Data()
      {
        System.IntPtr* dataPtr = au_vectorPoint2f_data(cvPtr);
        int size = Size();

        Point2f[] data = new Point2f[size];
        for (int i = 0; i < size; i++)
        {
          data[i] = new Point2f(dataPtr[i], DeleteResponsibility.False);
        }

        return data;
      }

      public void PushBack(Point2f value)
      {
        au_vectorPoint2f_push_back(cvPtr, value.cvPtr);
      }

      public int Size()
      {
        return au_vectorPoint2f_size(cvPtr);
      }
    }
  }
}