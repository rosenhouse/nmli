Documentation in progress :)


Changes from dnAnalytics:
 -IBlas<N>, ILapack<N>, and IVml<N> provide data-type agnostic functions.
   Look in src\NMLI\Extended for examples of code that uses these.
 -Bytes are used instead of chars at P/Invoke boundaries to avoid marshalling.
 -IBlas/ILapack/IVml functions do not allocate memory.
 -Higher-level functions (such as those in Extended) use ThreadStatic instances of
   Workspace<N> to amortize allocations.


See the code comments, the dnAnalytics page, or email me if you have questions.
