//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace av {

public class LibAvImage {
  public static ImageCapture createImageCapture() {
    global::System.IntPtr cPtr = LibAvImagePINVOKE.createImageCapture();
    ImageCapture ret = (cPtr == global::System.IntPtr.Zero) ? null : new ImageCapture(cPtr, true);
    return ret;
  }

}

}