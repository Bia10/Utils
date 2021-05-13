using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Utils.Marshalling
{
    public class MarshallingExtensions
    {
        public static T PtrToStructure<T>(IntPtr pointer) 
        {
            return (T)Marshal.PtrToStructure(pointer, typeof(T));
        }

        public static T[] PtrToStructureArray<T>(IntPtr pointer, int count) 
            where T : struct
        {
            var structure = new T[count];
            var structSize = Marshal.SizeOf(typeof(T));

            for (var i = 0; i < count; i++)
            {
                structure[i] = PtrToStructure<T>(pointer);
                pointer = IntPtr.Add(pointer, structSize);
            }

            return structure;
        }

        public static T ByteArrayToStructure<T>(byte[] bytes) 
            where T : struct
        {
            var handle = default(GCHandle);

            try
            {
                handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                var pointer = handle.AddrOfPinnedObject();
                var structure = PtrToStructure<T>(pointer);

                return structure;
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
        }

        public static T ByteArrayToStructure<T>(byte[] bytes, uint index) 
            where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var byteArray = new byte[size];

            Array.Copy(bytes, index, byteArray, 0, size);

            return ByteArrayToStructure<T>(byteArray);
        }

        public static T ByteArrayToStructure<T>(byte[] bytes, uint index, uint size) 
            where T : struct
        {
            var byteArray = new byte[size];

            Array.Copy(bytes, index, byteArray, 0, size);

            return ByteArrayToStructure<T>(byteArray);
        }

        public static T ByteArrayToStructure<T>(BinaryReader reader) 
            where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var byteArray = reader.ReadBytes(size);

            return ByteArrayToStructure<T>(byteArray);
        }
    }
}