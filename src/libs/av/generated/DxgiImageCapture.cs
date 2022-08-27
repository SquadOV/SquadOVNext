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

public class DxgiImageCapture : ImageCapture {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal DxgiImageCapture(global::System.IntPtr cPtr, bool cMemoryOwn) : base(LibAvImagePINVOKE.DxgiImageCapture_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(DxgiImageCapture obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          LibAvImagePINVOKE.delete_DxgiImageCapture(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public override NativeImage getCurrent() {
    NativeImage ret = new NativeImage(LibAvImagePINVOKE.DxgiImageCapture_getCurrent(swigCPtr), true);
    if (LibAvImagePINVOKE.SWIGPendingException.Pending) throw LibAvImagePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public DxgiImageCapture() : this(LibAvImagePINVOKE.new_DxgiImageCapture(), true) {
  }

}

}