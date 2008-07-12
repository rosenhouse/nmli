##******************************************************************************
##                              INTEL CONFIDENTIAL
##  Copyright(C) 2004-2008 Intel Corporation. All Rights Reserved.
##  The source code contained  or  described herein and all documents related to
##  the source code ("Material") are owned by Intel Corporation or its suppliers
##  or licensors.  Title to the  Material remains with  Intel Corporation or its
##  suppliers and licensors. The Material contains trade secrets and proprietary
##  and  confidential  information of  Intel or its suppliers and licensors. The
##  Material  is  protected  by  worldwide  copyright  and trade secret laws and
##  treaty  provisions. No part of the Material may be used, copied, reproduced,
##  modified, published, uploaded, posted, transmitted, distributed or disclosed
##  in any way without Intel's prior express written permission.
##  No license  under any  patent, copyright, trade secret or other intellectual
##  property right is granted to or conferred upon you by disclosure or delivery
##  of the Materials,  either expressly, by implication, inducement, estoppel or
##  otherwise.  Any  license  under  such  intellectual property  rights must be
##  express and approved by Intel in writing.
##
##******************************************************************************
##  Content:
##      Intel(R) Math Kernel Library Custom DLL builder
##
##******************************************************************************

help:
	@echo "Custom DLL builder."
	@echo "Usage: nmake {ia32/ipf/em64t} [options]"
	@echo "target ia32 for ia32, ipf for Intel(R) Itanium(R) processor family"
	@echo "em64t is used for Intel(R) Xeon(R) processor with Intel(R) EM64T."
	@echo "options:"
	@echo "interface=cdecl/stdcall - specifies interface for ia32, cdecl by default."
	@echo "interface=lp64/ilp64 - specifies interface for em64t and ipf, lp64 by default."
	@echo "threading=parallel/sequential, parallel by default."
	@echo "export=functions_list - name of file which contains functions list."
	@echo "name=mkl_custom - name of created libraries. mkl_custom.dll, mkl_custom.lib by default."
	@echo "xerbla=user_xerbla.obj - object file which contains error handler."
	@echo "BUF_LIB= - don't use bufferoverflowu.lib. Set this option "
	@echo "           if you use Microsoft Visual Studio .Net 2002, 2003, 2008"
	@echo "           or if you see 'fatal error LNK1181: cannot open input file 'bufferoverflowu.lib'' message."

##------------------------------------------------------------------------------
##
## examples of using:
## nmake ia32 - Build ia32 dll with cdecl interface.
##   Function list from "user_list". DLL name is mkl_custom.dll
##   No special xerbla is used.
##
## nmake ia32 interface=stdcall xerbla=my_xerbla.obj - Build ia32 DLL with
##   stdcall interface. With error handler from my_xerbla.obj.
##
## nmake ipf export=my_func_list.txt name=my_dll - Build dll for Intel(R) Itanium(R) processor family.
##   Function list from my_func_list.txt. DLL name is my_dll.
##   No special xerbla is used.
##
##------------------------------------------------------------------------------

## Path to mkl libraries (may be edited by user)

local_mklpath=C:\\PROGRA~1\\Intel\\MKL\\10.0.3.021\\
mkl32_libpath=$(local_mklpath)ia32\\lib\\
mklem64t_libpath=$(local_mklpath)em64t\\lib\\
mkl64_libpath=$(local_mklpath)ia64\\lib\\

##------------------------------------------------------------------------------
## Comment next line if you get the message:
## "LINK : fatal error LNK1181: cannot open input file 'bufferoverflowu.lib'"
##------------------------------------------------------------------------------
BUF_LIB=bufferoverflowu.lib

##------------------------------------------------------------------------------
## No changes below this line !
##------------------------------------------------------------------------------


!ifndef export
export="functions_list"
!endif

!ifndef name
name="mkl_custom"
!endif

IFACE_LIB_32=$(mkl32_libpath)mkl_intel_c.lib
!if "$(interface)"=="std" || "$(interface)"=="stdcall" || "$(interface)"=="STD" || "$(interface)"=="STDCALL"
IFACE_LIB_32=$(mkl32_libpath)mkl_intel_s.lib $(mkl32_libpath)mkl_intel_c.lib
!endif

IF=lp64
!if "$(interface)"=="ilp64"
IF=ilp64
!endif

!ifndef threading
threading=parallel
!endif

!if "$(threading)"=="sequential"
THREADING_LIB=mkl_sequential.lib
LIBGUIDE_64=
LIBGUIDE_EM64T=
LIBGUIDE_32=
!else
THREADING_LIB=mkl_intel_thread.lib
LIBGUIDE_64=$(mkl64_libpath)libguide40.lib
LIBGUIDE_EM64T=$(mklem64t_libpath)libguide40.lib
LIBGUIDE_32=$(mkl32_libpath)libguide40.lib
!endif


ipf:
	@echo EXPORTS > user_def_file.def
	@type $(export) >> user_def_file.def
	if exist .\temp_cdll rd /q /s .\temp_cdll
	md .\temp_cdll
	lib .\lib\mkl_cdll_ipf.lib /EXTRACT:vml_k7_strings.obj  /OUT:.\temp_cdll\vml_k7_strings.obj
	lib .\lib\mkl_cdll_ipf.lib /EXTRACT:vml_k7_errors.obj   /OUT:.\temp_cdll\vml_k7_errors.obj
	lib .\lib\mkl_cdll_ipf.lib /EXTRACT:vml_k7_mode.obj     /OUT:.\temp_cdll\vml_k7_mode.obj
	lib .\lib\mkl_cdll_ipf.lib /EXTRACT:i_malloc_dll.obj    /OUT:.\temp_cdll\i_malloc_dll.obj
	lib .\lib\mkl_cdll_ipf.lib /EXTRACT:_vml_dll_core.obj   /OUT:.\temp_cdll\_vml_dll_core.obj
	link  /DLL /MACHINE:IA64 /NODEFAULTLIB /def:user_def_file.def \
	.\temp_cdll\*.obj $(xerbla) \
	$(mkl64_libpath)mkl_intel_$(IF).lib \
	$(mkl64_libpath)$(THREADING_LIB) \
	$(mkl64_libpath)mkl_core.lib \
	$(mkl64_libpath)mkl_solver_lp64.lib \
	$(LIBGUIDE_64) \
	msvcrt.lib kernel32.lib user32.lib $(BUF_LIB) /out:$(name).dll
	rd /q /s .\temp_cdll
	@del user_def_file.def


em64t:
	@echo EXPORTS > user_def_file.def
	@type $(export) >> user_def_file.def
	if exist .\temp_cdll rd /q /s .\temp_cdll
	md .\temp_cdll
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:vml_ex_strings.obj  /OUT:.\temp_cdll\vml_ex_strings.obj
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:vml_ex_errors.obj   /OUT:.\temp_cdll\vml_ex_errors.obj
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:vml_ex_mode.obj     /OUT:.\temp_cdll\vml_ex_mode.obj
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:vml_e7_errors.obj   /OUT:.\temp_cdll\vml_e7_errors.obj
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:vml_e7_mode.obj     /OUT:.\temp_cdll\vml_e7_mode.obj
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:vml_u8_errors.obj   /OUT:.\temp_cdll\vml_e7_errors.obj
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:vml_u8_mode.obj     /OUT:.\temp_cdll\vml_e7_mode.obj
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:i_malloc_dll.obj    /OUT:.\temp_cdll\i_malloc_dll.obj
	lib .\lib\mkl_cdll_em64t.lib /EXTRACT:_vml_dll_core.obj   /OUT:.\temp_cdll\_vml_dll_core.obj
	link /DLL /MACHINE:AMD64 /NODEFAULTLIB /def:user_def_file.def \
	.\temp_cdll\*.obj $(xerbla) \
	$(mklem64t_libpath)mkl_intel_$(IF).lib \
	$(mklem64t_libpath)$(THREADING_LIB) \
	$(mklem64t_libpath)mkl_core.lib \
	$(mklem64t_libpath)mkl_solver_lp64.lib \
	$(LIBGUIDE_EM64T) \
	msvcrt.lib kernel32.lib user32.lib $(BUF_LIB) /out:$(name).dll
	rd /q /s .\temp_cdll
	@del user_def_file.def


ia32:
	@echo EXPORTS > user_def_file.def
	@type $(export) >> user_def_file.def
	if exist .\temp_cdll rd /q /s .\temp_cdll
	@md .\temp_cdll
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_a6_errors.obj   /OUT:.\temp_cdll\vml_a6_errors.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_a6_mode.obj     /OUT:.\temp_cdll\vml_a6_mode.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_px_errors.obj   /OUT:.\temp_cdll\vml_p6_errors.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_px_mode.obj     /OUT:.\temp_cdll\vml_p6_mode.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_t7_errors.obj   /OUT:.\temp_cdll\vml_t7_errors.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_t7_mode.obj     /OUT:.\temp_cdll\vml_t7_mode.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_w7_errors.obj   /OUT:.\temp_cdll\vml_w7_errors.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_w7_mode.obj     /OUT:.\temp_cdll\vml_w7_mode.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_v8_errors.obj   /OUT:.\temp_cdll\vml_v8_errors.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:vml_v8_mode.obj     /OUT:.\temp_cdll\vml_v8_mode.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:i_malloc_dll.obj    /OUT:.\temp_cdll\i_malloc_dll.obj
	@lib .\lib\mkl_cdll_ia32.lib /EXTRACT:_vml_dll_core.obj   /OUT:.\temp_cdll\_vml_dll_core.obj
	link  /DLL /MACHINE:IX86 /NODEFAULTLIB  /def:user_def_file.def \
	.\temp_cdll\*.obj $(xerbla) \
	$(IFACE_LIB_32) \
	$(mkl32_libpath)$(THREADING_LIB) \
	$(mkl32_libpath)mkl_core.lib \
	$(mkl32_libpath)mkl_solver.lib  \
	$(LIBGUIDE_32) \
	msvcrt.lib kernel32.lib user32.lib $(BUF_LIB) /out:$(name).dll
	@rd /q /s .\temp_cdll
	@del user_def_file.def
