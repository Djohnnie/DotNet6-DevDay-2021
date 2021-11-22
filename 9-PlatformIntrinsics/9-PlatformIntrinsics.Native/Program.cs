
using System.Runtime.InteropServices;

unsafe
{
    void* ptr1 = NativeMemory.Alloc(1024 * 1024 * 1024);
    NativeMemory.Free(ptr1);

    void* ptr2 = NativeMemory.AlignedAlloc(1024 * 1024 * 1024, 4);
    NativeMemory.AlignedFree(ptr2);
}