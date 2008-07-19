#!/bin/bash

NMLI_PATH=~/nmli
_mkl=/Library/Frameworks/Intel_MKL.framework/Versions/Current

cd $_mkl/tools/builder
make ia32 export=$NMLI_PATH/setup/function_list name=~/lib/mkl
cp $_mkl/lib/32/libguide.dylib ~/lib/
