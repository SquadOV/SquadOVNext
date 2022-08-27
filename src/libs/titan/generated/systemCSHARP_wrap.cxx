/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 4.0.2
 *
 * This file is not intended to be easily readable and contains a number of
 * coding conventions designed to improve portability and efficiency. Do not make
 * changes to this file unless you know what you are doing--modify the SWIG
 * interface file instead.
 * ----------------------------------------------------------------------------- */


#ifndef SWIGCSHARP
#define SWIGCSHARP
#endif



#ifdef __cplusplus
/* SwigValueWrapper is described in swig.swg */
template<typename T> class SwigValueWrapper {
  struct SwigMovePointer {
    T *ptr;
    SwigMovePointer(T *p) : ptr(p) { }
    ~SwigMovePointer() { delete ptr; }
    SwigMovePointer& operator=(SwigMovePointer& rhs) { T* oldptr = ptr; ptr = 0; delete oldptr; ptr = rhs.ptr; rhs.ptr = 0; return *this; }
  } pointer;
  SwigValueWrapper& operator=(const SwigValueWrapper<T>& rhs);
  SwigValueWrapper(const SwigValueWrapper<T>& rhs);
public:
  SwigValueWrapper() : pointer(0) { }
  SwigValueWrapper& operator=(const T& t) { SwigMovePointer tmp(new T(t)); pointer = tmp; return *this; }
  operator T&() const { return *pointer.ptr; }
  T *operator&() { return pointer.ptr; }
};

template <typename T> T SwigValueInit() {
  return T();
}
#endif

/* -----------------------------------------------------------------------------
 *  This section contains generic SWIG labels for method/variable
 *  declarations/attributes, and other compiler dependent labels.
 * ----------------------------------------------------------------------------- */

/* template workaround for compilers that cannot correctly implement the C++ standard */
#ifndef SWIGTEMPLATEDISAMBIGUATOR
# if defined(__SUNPRO_CC) && (__SUNPRO_CC <= 0x560)
#  define SWIGTEMPLATEDISAMBIGUATOR template
# elif defined(__HP_aCC)
/* Needed even with `aCC -AA' when `aCC -V' reports HP ANSI C++ B3910B A.03.55 */
/* If we find a maximum version that requires this, the test would be __HP_aCC <= 35500 for A.03.55 */
#  define SWIGTEMPLATEDISAMBIGUATOR template
# else
#  define SWIGTEMPLATEDISAMBIGUATOR
# endif
#endif

/* inline attribute */
#ifndef SWIGINLINE
# if defined(__cplusplus) || (defined(__GNUC__) && !defined(__STRICT_ANSI__))
#   define SWIGINLINE inline
# else
#   define SWIGINLINE
# endif
#endif

/* attribute recognised by some compilers to avoid 'unused' warnings */
#ifndef SWIGUNUSED
# if defined(__GNUC__)
#   if !(defined(__cplusplus)) || (__GNUC__ > 3 || (__GNUC__ == 3 && __GNUC_MINOR__ >= 4))
#     define SWIGUNUSED __attribute__ ((__unused__))
#   else
#     define SWIGUNUSED
#   endif
# elif defined(__ICC)
#   define SWIGUNUSED __attribute__ ((__unused__))
# else
#   define SWIGUNUSED
# endif
#endif

#ifndef SWIG_MSC_UNSUPPRESS_4505
# if defined(_MSC_VER)
#   pragma warning(disable : 4505) /* unreferenced local function has been removed */
# endif
#endif

#ifndef SWIGUNUSEDPARM
# ifdef __cplusplus
#   define SWIGUNUSEDPARM(p)
# else
#   define SWIGUNUSEDPARM(p) p SWIGUNUSED
# endif
#endif

/* internal SWIG method */
#ifndef SWIGINTERN
# define SWIGINTERN static SWIGUNUSED
#endif

/* internal inline SWIG method */
#ifndef SWIGINTERNINLINE
# define SWIGINTERNINLINE SWIGINTERN SWIGINLINE
#endif

/* exporting methods */
#if defined(__GNUC__)
#  if (__GNUC__ >= 4) || (__GNUC__ == 3 && __GNUC_MINOR__ >= 4)
#    ifndef GCC_HASCLASSVISIBILITY
#      define GCC_HASCLASSVISIBILITY
#    endif
#  endif
#endif

#ifndef SWIGEXPORT
# if defined(_WIN32) || defined(__WIN32__) || defined(__CYGWIN__)
#   if defined(STATIC_LINKED)
#     define SWIGEXPORT
#   else
#     define SWIGEXPORT __declspec(dllexport)
#   endif
# else
#   if defined(__GNUC__) && defined(GCC_HASCLASSVISIBILITY)
#     define SWIGEXPORT __attribute__ ((visibility("default")))
#   else
#     define SWIGEXPORT
#   endif
# endif
#endif

/* calling conventions for Windows */
#ifndef SWIGSTDCALL
# if defined(_WIN32) || defined(__WIN32__) || defined(__CYGWIN__)
#   define SWIGSTDCALL __stdcall
# else
#   define SWIGSTDCALL
# endif
#endif

/* Deal with Microsoft's attempt at deprecating C standard runtime functions */
#if !defined(SWIG_NO_CRT_SECURE_NO_DEPRECATE) && defined(_MSC_VER) && !defined(_CRT_SECURE_NO_DEPRECATE)
# define _CRT_SECURE_NO_DEPRECATE
#endif

/* Deal with Microsoft's attempt at deprecating methods in the standard C++ library */
#if !defined(SWIG_NO_SCL_SECURE_NO_DEPRECATE) && defined(_MSC_VER) && !defined(_SCL_SECURE_NO_DEPRECATE)
# define _SCL_SECURE_NO_DEPRECATE
#endif

/* Deal with Apple's deprecated 'AssertMacros.h' from Carbon-framework */
#if defined(__APPLE__) && !defined(__ASSERT_MACROS_DEFINE_VERSIONS_WITHOUT_UNDERSCORES)
# define __ASSERT_MACROS_DEFINE_VERSIONS_WITHOUT_UNDERSCORES 0
#endif

/* Intel's compiler complains if a variable which was never initialised is
 * cast to void, which is a common idiom which we use to indicate that we
 * are aware a variable isn't used.  So we just silence that warning.
 * See: https://github.com/swig/swig/issues/192 for more discussion.
 */
#ifdef __INTEL_COMPILER
# pragma warning disable 592
#endif


#include <stdlib.h>
#include <string.h>
#include <stdio.h>


/* Support for throwing C# exceptions from C/C++. There are two types: 
 * Exceptions that take a message and ArgumentExceptions that take a message and a parameter name. */
typedef enum {
  SWIG_CSharpApplicationException,
  SWIG_CSharpArithmeticException,
  SWIG_CSharpDivideByZeroException,
  SWIG_CSharpIndexOutOfRangeException,
  SWIG_CSharpInvalidCastException,
  SWIG_CSharpInvalidOperationException,
  SWIG_CSharpIOException,
  SWIG_CSharpNullReferenceException,
  SWIG_CSharpOutOfMemoryException,
  SWIG_CSharpOverflowException,
  SWIG_CSharpSystemException
} SWIG_CSharpExceptionCodes;

typedef enum {
  SWIG_CSharpArgumentException,
  SWIG_CSharpArgumentNullException,
  SWIG_CSharpArgumentOutOfRangeException
} SWIG_CSharpExceptionArgumentCodes;

typedef void (SWIGSTDCALL* SWIG_CSharpExceptionCallback_t)(const char *);
typedef void (SWIGSTDCALL* SWIG_CSharpExceptionArgumentCallback_t)(const char *, const char *);

typedef struct {
  SWIG_CSharpExceptionCodes code;
  SWIG_CSharpExceptionCallback_t callback;
} SWIG_CSharpException_t;

typedef struct {
  SWIG_CSharpExceptionArgumentCodes code;
  SWIG_CSharpExceptionArgumentCallback_t callback;
} SWIG_CSharpExceptionArgument_t;

static SWIG_CSharpException_t SWIG_csharp_exceptions[] = {
  { SWIG_CSharpApplicationException, NULL },
  { SWIG_CSharpArithmeticException, NULL },
  { SWIG_CSharpDivideByZeroException, NULL },
  { SWIG_CSharpIndexOutOfRangeException, NULL },
  { SWIG_CSharpInvalidCastException, NULL },
  { SWIG_CSharpInvalidOperationException, NULL },
  { SWIG_CSharpIOException, NULL },
  { SWIG_CSharpNullReferenceException, NULL },
  { SWIG_CSharpOutOfMemoryException, NULL },
  { SWIG_CSharpOverflowException, NULL },
  { SWIG_CSharpSystemException, NULL }
};

static SWIG_CSharpExceptionArgument_t SWIG_csharp_exceptions_argument[] = {
  { SWIG_CSharpArgumentException, NULL },
  { SWIG_CSharpArgumentNullException, NULL },
  { SWIG_CSharpArgumentOutOfRangeException, NULL }
};

static void SWIGUNUSED SWIG_CSharpSetPendingException(SWIG_CSharpExceptionCodes code, const char *msg) {
  SWIG_CSharpExceptionCallback_t callback = SWIG_csharp_exceptions[SWIG_CSharpApplicationException].callback;
  if ((size_t)code < sizeof(SWIG_csharp_exceptions)/sizeof(SWIG_CSharpException_t)) {
    callback = SWIG_csharp_exceptions[code].callback;
  }
  callback(msg);
}

static void SWIGUNUSED SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpExceptionArgumentCodes code, const char *msg, const char *param_name) {
  SWIG_CSharpExceptionArgumentCallback_t callback = SWIG_csharp_exceptions_argument[SWIG_CSharpArgumentException].callback;
  if ((size_t)code < sizeof(SWIG_csharp_exceptions_argument)/sizeof(SWIG_CSharpExceptionArgument_t)) {
    callback = SWIG_csharp_exceptions_argument[code].callback;
  }
  callback(msg, param_name);
}


#ifdef __cplusplus
extern "C" 
#endif
SWIGEXPORT void SWIGSTDCALL SWIGRegisterExceptionCallbacks_LibTitanSystem(
                                                SWIG_CSharpExceptionCallback_t applicationCallback,
                                                SWIG_CSharpExceptionCallback_t arithmeticCallback,
                                                SWIG_CSharpExceptionCallback_t divideByZeroCallback, 
                                                SWIG_CSharpExceptionCallback_t indexOutOfRangeCallback, 
                                                SWIG_CSharpExceptionCallback_t invalidCastCallback,
                                                SWIG_CSharpExceptionCallback_t invalidOperationCallback,
                                                SWIG_CSharpExceptionCallback_t ioCallback,
                                                SWIG_CSharpExceptionCallback_t nullReferenceCallback,
                                                SWIG_CSharpExceptionCallback_t outOfMemoryCallback, 
                                                SWIG_CSharpExceptionCallback_t overflowCallback, 
                                                SWIG_CSharpExceptionCallback_t systemCallback) {
  SWIG_csharp_exceptions[SWIG_CSharpApplicationException].callback = applicationCallback;
  SWIG_csharp_exceptions[SWIG_CSharpArithmeticException].callback = arithmeticCallback;
  SWIG_csharp_exceptions[SWIG_CSharpDivideByZeroException].callback = divideByZeroCallback;
  SWIG_csharp_exceptions[SWIG_CSharpIndexOutOfRangeException].callback = indexOutOfRangeCallback;
  SWIG_csharp_exceptions[SWIG_CSharpInvalidCastException].callback = invalidCastCallback;
  SWIG_csharp_exceptions[SWIG_CSharpInvalidOperationException].callback = invalidOperationCallback;
  SWIG_csharp_exceptions[SWIG_CSharpIOException].callback = ioCallback;
  SWIG_csharp_exceptions[SWIG_CSharpNullReferenceException].callback = nullReferenceCallback;
  SWIG_csharp_exceptions[SWIG_CSharpOutOfMemoryException].callback = outOfMemoryCallback;
  SWIG_csharp_exceptions[SWIG_CSharpOverflowException].callback = overflowCallback;
  SWIG_csharp_exceptions[SWIG_CSharpSystemException].callback = systemCallback;
}

#ifdef __cplusplus
extern "C" 
#endif
SWIGEXPORT void SWIGSTDCALL SWIGRegisterExceptionArgumentCallbacks_LibTitanSystem(
                                                SWIG_CSharpExceptionArgumentCallback_t argumentCallback,
                                                SWIG_CSharpExceptionArgumentCallback_t argumentNullCallback,
                                                SWIG_CSharpExceptionArgumentCallback_t argumentOutOfRangeCallback) {
  SWIG_csharp_exceptions_argument[SWIG_CSharpArgumentException].callback = argumentCallback;
  SWIG_csharp_exceptions_argument[SWIG_CSharpArgumentNullException].callback = argumentNullCallback;
  SWIG_csharp_exceptions_argument[SWIG_CSharpArgumentOutOfRangeException].callback = argumentOutOfRangeCallback;
}


/* Callback for returning strings to C# without leaking memory */
typedef char * (SWIGSTDCALL* SWIG_CSharpStringHelperCallback)(const char *);
static SWIG_CSharpStringHelperCallback SWIG_csharp_string_callback = NULL;


#ifdef __cplusplus
extern "C" 
#endif
SWIGEXPORT void SWIGSTDCALL SWIGRegisterStringCallback_LibTitanSystem(SWIG_CSharpStringHelperCallback callback) {
  SWIG_csharp_string_callback = callback;
}


/* Contract support */

#define SWIG_contract_assert(nullreturn, expr, msg) if (!(expr)) {SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, msg, ""); return nullreturn; } else


#include "titan/system/process.h"
#include "titan/system/process_di.h"


#include <string>


#include <typeinfo>
#include <stdexcept>


#include <vector>
#include <algorithm>
#include <stdexcept>


struct SWIG_null_deleter {
  void operator() (void const *) const {
  }
};
#define SWIG_NO_NULL_DELETER_0 , SWIG_null_deleter()
#define SWIG_NO_NULL_DELETER_1
#define SWIG_NO_NULL_DELETER_SWIG_POINTER_NEW
#define SWIG_NO_NULL_DELETER_SWIG_POINTER_OWN

SWIGINTERN std::vector< titan::system::Process > *new_std_vector_Sl_titan_system_Process_Sg___SWIG_2(int capacity){
        std::vector< titan::system::Process >* pv = 0;
        if (capacity >= 0) {
          pv = new std::vector< titan::system::Process >();
          pv->reserve(capacity);
       } else {
          throw std::out_of_range("capacity");
       }
       return pv;
      }
SWIGINTERN titan::system::Process std_vector_Sl_titan_system_Process_Sg__getitemcopy(std::vector< titan::system::Process > *self,int index){
        if (index>=0 && index<(int)self->size())
          return (*self)[index];
        else
          throw std::out_of_range("index");
      }
SWIGINTERN std::vector< titan::system::Process >::value_type const &std_vector_Sl_titan_system_Process_Sg__getitem(std::vector< titan::system::Process > *self,int index){
        if (index>=0 && index<(int)self->size())
          return (*self)[index];
        else
          throw std::out_of_range("index");
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__setitem(std::vector< titan::system::Process > *self,int index,titan::system::Process const &val){
        if (index>=0 && index<(int)self->size())
          (*self)[index] = val;
        else
          throw std::out_of_range("index");
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__AddRange(std::vector< titan::system::Process > *self,std::vector< titan::system::Process > const &values){
        self->insert(self->end(), values.begin(), values.end());
      }
SWIGINTERN std::vector< titan::system::Process > *std_vector_Sl_titan_system_Process_Sg__GetRange(std::vector< titan::system::Process > *self,int index,int count){
        if (index < 0)
          throw std::out_of_range("index");
        if (count < 0)
          throw std::out_of_range("count");
        if (index >= (int)self->size()+1 || index+count > (int)self->size())
          throw std::invalid_argument("invalid range");
        return new std::vector< titan::system::Process >(self->begin()+index, self->begin()+index+count);
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__Insert(std::vector< titan::system::Process > *self,int index,titan::system::Process const &x){
        if (index>=0 && index<(int)self->size()+1)
          self->insert(self->begin()+index, x);
        else
          throw std::out_of_range("index");
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__InsertRange(std::vector< titan::system::Process > *self,int index,std::vector< titan::system::Process > const &values){
        if (index>=0 && index<(int)self->size()+1)
          self->insert(self->begin()+index, values.begin(), values.end());
        else
          throw std::out_of_range("index");
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__RemoveAt(std::vector< titan::system::Process > *self,int index){
        if (index>=0 && index<(int)self->size())
          self->erase(self->begin() + index);
        else
          throw std::out_of_range("index");
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__RemoveRange(std::vector< titan::system::Process > *self,int index,int count){
        if (index < 0)
          throw std::out_of_range("index");
        if (count < 0)
          throw std::out_of_range("count");
        if (index >= (int)self->size()+1 || index+count > (int)self->size())
          throw std::invalid_argument("invalid range");
        self->erase(self->begin()+index, self->begin()+index+count);
      }
SWIGINTERN std::vector< titan::system::Process > *std_vector_Sl_titan_system_Process_Sg__Repeat(titan::system::Process const &value,int count){
        if (count < 0)
          throw std::out_of_range("count");
        return new std::vector< titan::system::Process >(count, value);
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__Reverse__SWIG_0(std::vector< titan::system::Process > *self){
        std::reverse(self->begin(), self->end());
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__Reverse__SWIG_1(std::vector< titan::system::Process > *self,int index,int count){
        if (index < 0)
          throw std::out_of_range("index");
        if (count < 0)
          throw std::out_of_range("count");
        if (index >= (int)self->size()+1 || index+count > (int)self->size())
          throw std::invalid_argument("invalid range");
        std::reverse(self->begin()+index, self->begin()+index+count);
      }
SWIGINTERN void std_vector_Sl_titan_system_Process_Sg__SetRange(std::vector< titan::system::Process > *self,int index,std::vector< titan::system::Process > const &values){
        if (index < 0)
          throw std::out_of_range("index");
        if (index+values.size() > self->size())
          throw std::out_of_range("index");
        std::copy(values.begin(), values.end(), self->begin()+index);
      }

#ifdef __cplusplus
extern "C" {
#endif

SWIGEXPORT void SWIGSTDCALL CSharp_titan_delete_NativeProcessDI(void * jarg1) {
  titan::system::NativeProcessDI *arg1 = (titan::system::NativeProcessDI *) 0 ;
  std::shared_ptr< titan::system::NativeProcessDI > *smartarg1 = 0 ;
  
  
  smartarg1 = (std::shared_ptr<  titan::system::NativeProcessDI > *)jarg1;
  arg1 = (titan::system::NativeProcessDI *)(smartarg1 ? smartarg1->get() : 0); 
  (void)arg1; delete smartarg1;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_NativeProcessDI_enumProcesses(void * jarg1) {
  void * jresult ;
  titan::system::NativeProcessDI *arg1 = (titan::system::NativeProcessDI *) 0 ;
  std::shared_ptr< titan::system::NativeProcessDI > *smartarg1 = 0 ;
  SwigValueWrapper< std::vector< NativeProcessId > > result;
  
  
  smartarg1 = (std::shared_ptr<  titan::system::NativeProcessDI > *)jarg1;
  arg1 = (titan::system::NativeProcessDI *)(smartarg1 ? smartarg1->get() : 0); 
  result = (arg1)->enumProcesses();
  jresult = new std::vector< NativeProcessId >((const std::vector< NativeProcessId > &)result); 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_NativeProcessDI_getProcessPath(void * jarg1, void * jarg2) {
  void * jresult ;
  titan::system::NativeProcessDI *arg1 = (titan::system::NativeProcessDI *) 0 ;
  NativeProcessHandle arg2 ;
  std::shared_ptr< titan::system::NativeProcessDI > *smartarg1 = 0 ;
  NativeProcessHandle *argp2 ;
  NativeString result;
  
  
  smartarg1 = (std::shared_ptr<  titan::system::NativeProcessDI > *)jarg1;
  arg1 = (titan::system::NativeProcessDI *)(smartarg1 ? smartarg1->get() : 0); 
  argp2 = (NativeProcessHandle *)jarg2; 
  if (!argp2) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "Attempt to dereference null NativeProcessHandle", 0);
    return 0;
  }
  arg2 = *argp2; 
  result = (arg1)->getProcessPath(arg2);
  jresult = new NativeString((const NativeString &)result); 
  return jresult;
}


SWIGEXPORT char * SWIGSTDCALL CSharp_titan_NativeProcessDI_getProcessFriendlyName(void * jarg1, void * jarg2) {
  char * jresult ;
  titan::system::NativeProcessDI *arg1 = (titan::system::NativeProcessDI *) 0 ;
  NativeString *arg2 = 0 ;
  std::shared_ptr< titan::system::NativeProcessDI > *smartarg1 = 0 ;
  std::string result;
  
  
  smartarg1 = (std::shared_ptr<  titan::system::NativeProcessDI > *)jarg1;
  arg1 = (titan::system::NativeProcessDI *)(smartarg1 ? smartarg1->get() : 0); 
  arg2 = (NativeString *)jarg2;
  if (!arg2) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "NativeString const & type is null", 0);
    return 0;
  } 
  result = (arg1)->getProcessFriendlyName((NativeString const &)*arg2);
  jresult = SWIG_csharp_string_callback((&result)->c_str()); 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_NativeProcessDI_getProcessStartTime(void * jarg1, void * jarg2) {
  void * jresult ;
  titan::system::NativeProcessDI *arg1 = (titan::system::NativeProcessDI *) 0 ;
  NativeProcessHandle arg2 ;
  std::shared_ptr< titan::system::NativeProcessDI > *smartarg1 = 0 ;
  NativeProcessHandle *argp2 ;
  int64_t result;
  
  
  smartarg1 = (std::shared_ptr<  titan::system::NativeProcessDI > *)jarg1;
  arg1 = (titan::system::NativeProcessDI *)(smartarg1 ? smartarg1->get() : 0); 
  argp2 = (NativeProcessHandle *)jarg2; 
  if (!argp2) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "Attempt to dereference null NativeProcessHandle", 0);
    return 0;
  }
  arg2 = *argp2; 
  result = (arg1)->getProcessStartTime(arg2);
  jresult = new int64_t((const int64_t &)result); 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_new_NativeProcessDI() {
  void * jresult ;
  titan::system::NativeProcessDI *result = 0 ;
  
  result = (titan::system::NativeProcessDI *)new titan::system::NativeProcessDI();
  
  jresult = result ? new std::shared_ptr<  titan::system::NativeProcessDI >(result SWIG_NO_NULL_DELETER_1) : 0;
  
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_getDefaultNativeProcessDI() {
  void * jresult ;
  titan::system::NativeProcessDIPtr result;
  
  result = titan::system::getDefaultNativeProcessDI();
  jresult = result ? new titan::system::NativeProcessDIPtr(result) : 0; 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_new_Process__SWIG_0(void * jarg1, void * jarg2) {
  void * jresult ;
  NativeProcessId arg1 ;
  titan::system::NativeProcessDIPtr *arg2 = 0 ;
  NativeProcessId *argp1 ;
  titan::system::NativeProcessDIPtr tempnull2 ;
  titan::system::Process *result = 0 ;
  
  argp1 = (NativeProcessId *)jarg1; 
  if (!argp1) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "Attempt to dereference null NativeProcessId", 0);
    return 0;
  }
  arg1 = *argp1; 
  arg2 = jarg2 ? (titan::system::NativeProcessDIPtr *)jarg2 : &tempnull2; 
  result = (titan::system::Process *)new titan::system::Process(arg1,(titan::system::NativeProcessDIPtr const &)*arg2);
  jresult = (void *)result; 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_new_Process__SWIG_1(void * jarg1) {
  void * jresult ;
  NativeProcessId arg1 ;
  NativeProcessId *argp1 ;
  titan::system::Process *result = 0 ;
  
  argp1 = (NativeProcessId *)jarg1; 
  if (!argp1) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "Attempt to dereference null NativeProcessId", 0);
    return 0;
  }
  arg1 = *argp1; 
  result = (titan::system::Process *)new titan::system::Process(arg1);
  jresult = (void *)result; 
  return jresult;
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_delete_Process(void * jarg1) {
  titan::system::Process *arg1 = (titan::system::Process *) 0 ;
  
  arg1 = (titan::system::Process *)jarg1; 
  delete arg1;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_loadRunningProcesses__SWIG_0(void * jarg1) {
  void * jresult ;
  titan::system::NativeProcessDIPtr *arg1 = 0 ;
  titan::system::NativeProcessDIPtr tempnull1 ;
  std::vector< titan::system::Process > result;
  
  arg1 = jarg1 ? (titan::system::NativeProcessDIPtr *)jarg1 : &tempnull1; 
  result = titan::system::loadRunningProcesses((std::shared_ptr< titan::system::NativeProcessDI > const &)*arg1);
  jresult = new std::vector< titan::system::Process >((const std::vector< titan::system::Process > &)result); 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_loadRunningProcesses__SWIG_1() {
  void * jresult ;
  std::vector< titan::system::Process > result;
  
  result = titan::system::loadRunningProcesses();
  jresult = new std::vector< titan::system::Process >((const std::vector< titan::system::Process > &)result); 
  return jresult;
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_Clear(void * jarg1) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  (arg1)->clear();
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_Add(void * jarg1, void * jarg2) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  titan::system::Process *arg2 = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (titan::system::Process *)jarg2;
  if (!arg2) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "titan::system::Process const & type is null", 0);
    return ;
  } 
  (arg1)->push_back((titan::system::Process const &)*arg2);
}


SWIGEXPORT unsigned long SWIGSTDCALL CSharp_titan_ProcessVector_size(void * jarg1) {
  unsigned long jresult ;
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  std::vector< titan::system::Process >::size_type result;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  result = ((std::vector< titan::system::Process > const *)arg1)->size();
  jresult = (unsigned long)result; 
  return jresult;
}


SWIGEXPORT unsigned long SWIGSTDCALL CSharp_titan_ProcessVector_capacity(void * jarg1) {
  unsigned long jresult ;
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  std::vector< titan::system::Process >::size_type result;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  result = ((std::vector< titan::system::Process > const *)arg1)->capacity();
  jresult = (unsigned long)result; 
  return jresult;
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_reserve(void * jarg1, unsigned long jarg2) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  std::vector< titan::system::Process >::size_type arg2 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (std::vector< titan::system::Process >::size_type)jarg2; 
  (arg1)->reserve(arg2);
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_new_ProcessVector__SWIG_0() {
  void * jresult ;
  std::vector< titan::system::Process > *result = 0 ;
  
  result = (std::vector< titan::system::Process > *)new std::vector< titan::system::Process >();
  jresult = (void *)result; 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_new_ProcessVector__SWIG_1(void * jarg1) {
  void * jresult ;
  std::vector< titan::system::Process > *arg1 = 0 ;
  std::vector< titan::system::Process > *result = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1;
  if (!arg1) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "std::vector< titan::system::Process > const & type is null", 0);
    return 0;
  } 
  result = (std::vector< titan::system::Process > *)new std::vector< titan::system::Process >((std::vector< titan::system::Process > const &)*arg1);
  jresult = (void *)result; 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_new_ProcessVector__SWIG_2(int jarg1) {
  void * jresult ;
  int arg1 ;
  std::vector< titan::system::Process > *result = 0 ;
  
  arg1 = (int)jarg1; 
  try {
    result = (std::vector< titan::system::Process > *)new_std_vector_Sl_titan_system_Process_Sg___SWIG_2(arg1);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return 0;
  }
  jresult = (void *)result; 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_ProcessVector_getitemcopy(void * jarg1, int jarg2) {
  void * jresult ;
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  SwigValueWrapper< titan::system::Process > result;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  try {
    result = std_vector_Sl_titan_system_Process_Sg__getitemcopy(arg1,arg2);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return 0;
  }
  jresult = new titan::system::Process((const titan::system::Process &)result); 
  return jresult;
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_ProcessVector_getitem(void * jarg1, int jarg2) {
  void * jresult ;
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  std::vector< titan::system::Process >::value_type *result = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  try {
    result = (std::vector< titan::system::Process >::value_type *) &std_vector_Sl_titan_system_Process_Sg__getitem(arg1,arg2);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return 0;
  }
  jresult = (void *)result; 
  return jresult;
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_setitem(void * jarg1, int jarg2, void * jarg3) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  titan::system::Process *arg3 = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  arg3 = (titan::system::Process *)jarg3;
  if (!arg3) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "titan::system::Process const & type is null", 0);
    return ;
  } 
  try {
    std_vector_Sl_titan_system_Process_Sg__setitem(arg1,arg2,(titan::system::Process const &)*arg3);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return ;
  }
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_AddRange(void * jarg1, void * jarg2) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  std::vector< titan::system::Process > *arg2 = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (std::vector< titan::system::Process > *)jarg2;
  if (!arg2) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "std::vector< titan::system::Process > const & type is null", 0);
    return ;
  } 
  std_vector_Sl_titan_system_Process_Sg__AddRange(arg1,(std::vector< titan::system::Process > const &)*arg2);
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_ProcessVector_GetRange(void * jarg1, int jarg2, int jarg3) {
  void * jresult ;
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  int arg3 ;
  std::vector< titan::system::Process > *result = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  arg3 = (int)jarg3; 
  try {
    result = (std::vector< titan::system::Process > *)std_vector_Sl_titan_system_Process_Sg__GetRange(arg1,arg2,arg3);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return 0;
  } catch(std::invalid_argument &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentException, (&_e)->what(), "");
    return 0;
  }
  jresult = (void *)result; 
  return jresult;
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_Insert(void * jarg1, int jarg2, void * jarg3) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  titan::system::Process *arg3 = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  arg3 = (titan::system::Process *)jarg3;
  if (!arg3) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "titan::system::Process const & type is null", 0);
    return ;
  } 
  try {
    std_vector_Sl_titan_system_Process_Sg__Insert(arg1,arg2,(titan::system::Process const &)*arg3);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return ;
  }
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_InsertRange(void * jarg1, int jarg2, void * jarg3) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  std::vector< titan::system::Process > *arg3 = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  arg3 = (std::vector< titan::system::Process > *)jarg3;
  if (!arg3) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "std::vector< titan::system::Process > const & type is null", 0);
    return ;
  } 
  try {
    std_vector_Sl_titan_system_Process_Sg__InsertRange(arg1,arg2,(std::vector< titan::system::Process > const &)*arg3);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return ;
  }
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_RemoveAt(void * jarg1, int jarg2) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  try {
    std_vector_Sl_titan_system_Process_Sg__RemoveAt(arg1,arg2);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return ;
  }
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_RemoveRange(void * jarg1, int jarg2, int jarg3) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  int arg3 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  arg3 = (int)jarg3; 
  try {
    std_vector_Sl_titan_system_Process_Sg__RemoveRange(arg1,arg2,arg3);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return ;
  } catch(std::invalid_argument &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentException, (&_e)->what(), "");
    return ;
  }
}


SWIGEXPORT void * SWIGSTDCALL CSharp_titan_ProcessVector_Repeat(void * jarg1, int jarg2) {
  void * jresult ;
  titan::system::Process *arg1 = 0 ;
  int arg2 ;
  std::vector< titan::system::Process > *result = 0 ;
  
  arg1 = (titan::system::Process *)jarg1;
  if (!arg1) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "titan::system::Process const & type is null", 0);
    return 0;
  } 
  arg2 = (int)jarg2; 
  try {
    result = (std::vector< titan::system::Process > *)std_vector_Sl_titan_system_Process_Sg__Repeat((titan::system::Process const &)*arg1,arg2);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return 0;
  }
  jresult = (void *)result; 
  return jresult;
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_Reverse__SWIG_0(void * jarg1) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  std_vector_Sl_titan_system_Process_Sg__Reverse__SWIG_0(arg1);
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_Reverse__SWIG_1(void * jarg1, int jarg2, int jarg3) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  int arg3 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  arg3 = (int)jarg3; 
  try {
    std_vector_Sl_titan_system_Process_Sg__Reverse__SWIG_1(arg1,arg2,arg3);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return ;
  } catch(std::invalid_argument &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentException, (&_e)->what(), "");
    return ;
  }
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_ProcessVector_SetRange(void * jarg1, int jarg2, void * jarg3) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  int arg2 ;
  std::vector< titan::system::Process > *arg3 = 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  arg2 = (int)jarg2; 
  arg3 = (std::vector< titan::system::Process > *)jarg3;
  if (!arg3) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentNullException, "std::vector< titan::system::Process > const & type is null", 0);
    return ;
  } 
  try {
    std_vector_Sl_titan_system_Process_Sg__SetRange(arg1,arg2,(std::vector< titan::system::Process > const &)*arg3);
  } catch(std::out_of_range &_e) {
    SWIG_CSharpSetPendingExceptionArgument(SWIG_CSharpArgumentOutOfRangeException, 0, (&_e)->what());
    return ;
  }
}


SWIGEXPORT void SWIGSTDCALL CSharp_titan_delete_ProcessVector(void * jarg1) {
  std::vector< titan::system::Process > *arg1 = (std::vector< titan::system::Process > *) 0 ;
  
  arg1 = (std::vector< titan::system::Process > *)jarg1; 
  delete arg1;
}


#ifdef __cplusplus
}
#endif

