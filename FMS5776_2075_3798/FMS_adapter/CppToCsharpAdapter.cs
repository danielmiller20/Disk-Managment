﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FMS_adapter
{
    public enum SLEVEL { user = 1, Administrator, Super_User, Owner };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class VHD
    {
        uint sectorNr;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        string diskName;
        public string DiskName { get { return diskName; } }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        string diskOwner;
        public string DiskOwner { get { return diskOwner; } }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        string prodDate;
        public string ProdDate { get { return prodDate; } }

        uint clusQty;
        public uint ClusQty { get { return clusQty; } }

        uint dataClusQty;
        public uint DataClusQty { get { return dataClusQty; } }

        uint addrDAT;
        public uint AddrDAT { get { return addrDAT; } }

        uint addrRootDir;
        public uint AddrRootDir { get { return addrRootDir; } }

        uint addrDATcpy;
        public uint AddrDATcpy { get { return addrDATcpy; } }

        uint addrRootDirCpy;
        public uint AddrRootDirCpy { get { return addrRootDirCpy; } }

        uint addrDataStart;
        public uint aAddrDataStart { get { return addrDataStart; } }

        uint addrUserSec;
        public uint AddrUserDate { get { return addrUserSec; } }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        string formatDate;
        public string FormatDate { get { return formatDate; } }

        [MarshalAs(UnmanagedType.I1)]
        bool isFormated;
        public bool IsFormated { get { return isFormated; } }


        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 940)]
        string emptyArea;
    }

    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class RecEntry
    {
        int recNr;
        public int RecNr { get { return recNr; } }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        string key;
        public string Key { get { return key; } }
        
    }

    
   
    class cppToCsharpAdapter
    {
        const string dllPath = "FMS_DLL.dll";

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int sum(int a, int b);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getfileDesc(IntPtr THIS, IntPtr Dir);

        // init disk
        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr makeDiskObject();

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void deleteDiskObject(ref IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getLastDiskErrorMessage(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getLastFcbErrorMessage(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void deleteFcbObject(ref IntPtr THIS);

        // Level 0

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void createDisk(IntPtr THIS, string diskName, string diskOwner, string pwd);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mountDisk(IntPtr THIS, string fileName);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void unmountDisk(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void recreateDisk(IntPtr THIS, string diskOwner);


        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void addUser(IntPtr THIS, string user, SLEVEL sLevel, string pwd, SLEVEL applicantSLevel);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void signIn(IntPtr THIS, string user, string pwd);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void signOut(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void removeUser(IntPtr THIS, string user, string pwd);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void removeUserSigned(IntPtr THIS, string user, SLEVEL applicantSLevel);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void changePassword(IntPtr THIS, string user, string oldPwd, string newPwd);
        // Level 1
        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void format(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int howMuchEmpty(IntPtr THIS);

        //Level 2
        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void createFile(IntPtr THIS, string fileName, string recFormat,
                                uint recSize, uint recNum, uint numOfSecs,
                                string keyType, SLEVEL slevel, uint keyOffset, uint keySize = 4, uint algo = 0);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void delFile(IntPtr THIS, string fileName);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void extendFile(IntPtr THIS, string fileName, uint size);


        // Level 3
        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr openFile(IntPtr THIS, string fileName, MODE mo);


        // FCB
        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void closeFile(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void readRecord(IntPtr THIS, IntPtr dest, uint readForUpdate = 0);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void addRecord(IntPtr THIS, IntPtr source);


        //[DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void seekRec(IntPtr THIS, uint from, int pos);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void updateRecCancel(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void deleteRecord(IntPtr THIS, uint readForUpdate);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void updateRecord(IntPtr THIS, IntPtr source);

        //Level 4
        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr makeStudentObject();

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void deleteStudentObject(ref IntPtr myStudentPtr);

        //extra
        //[DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        //[return: MarshalAs(UnmanagedType.AnsiBStr)]
        //public static extern string getDat(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getVHD(IntPtr THIS, IntPtr buffer);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getCU(IntPtr THIS, IntPtr buffer);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getRootDir(IntPtr THIS, IntPtr buffer);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getDirEntry(IntPtr THIS, IntPtr buffer, int index);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void getRecEntry(IntPtr THIS, IntPtr buffer, int index);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint getRecInfoSize(IntPtr THIS);
        

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint getNumOfRecords(IntPtr THIS);

        [DllImport(dllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int debug();



        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        //public class FileHeader
        //{
        //    uint sectorNr;

        //    DirEntry fileDesc;
        //    public DirEntry FileDesc { get { return fileDesc; } }

        //    RecInfo recInfo;
        //    public RecInfo RecInfo { get { return recInfo; } }

        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        //    string emptyArea;
        //    public string EmptyArea { get { return emptyArea; } }
        //}

    }
}
