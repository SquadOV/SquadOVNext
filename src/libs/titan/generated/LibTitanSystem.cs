//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace titan {

public class LibTitanSystem {
  public static ProcessVector loadRunningProcesses() {
    ProcessVector ret = new ProcessVector(LibTitanSystemPINVOKE.loadRunningProcesses(), true);
    return ret;
  }

}

}