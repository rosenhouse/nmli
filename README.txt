Code and documentation in progress :)


Overview:

Major changes from dnAnalytics:
 -IBlas<N>, ILapack<N>, and IVml<N> provide data-type agnostic functions.
   Look in src\NMLI\Extended for examples of code that uses these.
 -Bytes are used instead of chars at P/Invoke boundaries to avoid marshalling.
 -IBlas/ILapack/IVml functions do not allocate memory.
 -Higher-level functions (such as those in Extended) use ThreadStatic instances of
   Workspace<N> to amortize allocations.


No where near 100% unit test coverage.  But the generic-numerics support allows 
for writing a single generic unit test for most functions.  Some subclassing trickery
then gets NUnit to run the test once for each of the four datatype*implementation pairs.
Achieving full coverage standardizing the test layout using the GenericNumericTest<_,_>
class is on my short list.


There's also some cool experimental stuff (well, more experimental than the rest)
to allow for BLAS and VML calls into the middle of a .NET array by taking advantage 
of unsafe C# support for pointers.  See the WithOffsets folder, and the OA<_> type.

To everything implementing the same interfaces, we end up abstracting out 
the collection type itself (.NET array vs pointer) along with the elemental data type
via the interfaces in the CollectionAgnostic folder.  I love abstraction.



Setup:

Setup/configuration of the native libraries can be non-trivial.  
See the setup subfolder.




Vector math library annoyances:

The MKL provides a lot more functionality than the ACML.
This is frustrating, because I want to support everything in MKL, but clients of NMLI 
should be able to remain native-library agnostic if they wish. Currently I've put some 
ACML-unsupported functions in the common IVml interface, and implemented managed 
substitutes that the ACML class passes off as its own.  This needs to be fixed.  
I will probably cut down the IVml interface to only those functions common to both, but leave
the MKL-only functions available elsewhere.  Perhaps the client can elect to either receive
the managed subsitutes, or just get an exception, in those cases where an unavailable function
is requested.



Email me (Gabriel) if you have questions:
   <googlecode project owner>@gmail.com


