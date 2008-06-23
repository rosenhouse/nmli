Native Math Library Interface

The documentation is currently non-existent.  See the code comments, the dnAnalytics page, or email me if you have questions.


The Native Math Library Interface (NMLI) is a fork of the dnAnalytics NLI with some slightly different design goals, but the same basic aim: To provide a thin .NET wrapper around the Intel Math Kernel Library (MKL) and the AMD Core Math Library (ACML).

NMLI aims to have

    * Polymorphic interfaces: Client code can be entirely data-type agnostic--compile one generic function, and pass it either singles or doubles.
    * Full support for IA32 and x64 versions of MKL and ACML.
    * Minimal performance overhead: Low-level wrappers do not marshal data or allocate memory. High-level functions amortize allocations in a thread-safe way. 

Currently only a subset of BLAS, LAPACK and the respective vector-math functions (Exp, Log, etc) are implemented. More functions will be added as the need arises (or patches come in). NMLI is currently untested on Mono.


Many thanks to Marcus & Patrick for their terrific work on dnAnalytics.

dnAnalytics: http://www.codeplex.com/dnAnalytics
NLI: http://www.codeplex.com/dnAnalytics/Wiki/View.aspx?title=NativeLibraryInterfaceOverview&referringTitle=Home




