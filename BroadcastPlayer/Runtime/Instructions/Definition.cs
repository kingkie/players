using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSetter.Runtime.Instructions
{



    class Definition
    {
        public static Dictionary<int, int> CFG_DBI = new Dictionary<int, int>
        {
            { (int)DeviceBroadcastInstructions.REQ_PlaySmell ,6},
            { (int)DeviceBroadcastInstructions.REQ_StopPlay ,0},
            { (int)DeviceBroadcastInstructions.REQ_PLayScript ,4},
            { (int)DeviceBroadcastInstructions.REQ_GetFirmwareVerison ,0},
            { (int)DeviceBroadcastInstructions.REQ_ScriptLoadStart ,8},
            { (int)DeviceBroadcastInstructions.REQ_ScriptLoading ,16},
            { (int)DeviceBroadcastInstructions.REQ_ScriptLoaded ,16},
            { (int)DeviceBroadcastInstructions.REQ_StatusQuery ,0},
            { (int)DeviceBroadcastInstructions.REQ_SetChannel ,13},
            { (int)DeviceBroadcastInstructions.REQ_AssignAddress ,14},

            { (int)DeviceBroadcastInstructions.REQ_SetDevicePhysicalChannel ,13 },
            { (int)DeviceBroadcastInstructions.RESP_SetDevicePhysicalChannel ,13 },

            { (int)DeviceBroadcastInstructions.RESP_GetFirmwareVerison ,4},
            { (int)DeviceBroadcastInstructions.RESP_ScriptQuery ,17},
            { (int)DeviceBroadcastInstructions.RESP_StatusQuery ,7},
            { (int)DeviceBroadcastInstructions.RESP_SetChannel ,13},
            { (int)DeviceBroadcastInstructions.RESP_AssignAddress ,13},

            { (int)DeviceBroadcastInstructions.RESP_StatusQueryAll ,41},





            { (int)DeviceBroadcastInstructions.REQ_SetDeviceBroadCastBaudRate ,1},


        };


        public static Dictionary<int, int> CFG_CSPI = new Dictionary<int, int>()
        {
            { (int)ControllerSerialPortInstructions.REQ_ReadControllerChannel ,0},
            { (int)ControllerSerialPortInstructions.REQ_AssignPhysicalChannel ,2},
            { (int)ControllerSerialPortInstructions.REQ_AssignControllerPhysicalChannel ,2},
            { (int)ControllerSerialPortInstructions.REQ_GetControllerPhysicalChannel ,0},
            { (int)ControllerSerialPortInstructions.REQ_GetFirmwareVerison ,0},

            { (int)ControllerSerialPortInstructions.REQ_DeviceSyncScript ,5},



            { (int)ControllerSerialPortInstructions.RESP_ReadControllerChannel ,1},
            { (int)ControllerSerialPortInstructions.RESP_AssignControllerPhysicalChannel ,1},
            { (int)ControllerSerialPortInstructions.RESP_GetControllerPhysicalChannel ,1},
            { (int)ControllerSerialPortInstructions.RESP_GetFirmwareVerison ,4},

            { (int)ControllerSerialPortInstructions.REQ_RFIDReadDeviceUIDAndSequenceID ,0},
            { (int)ControllerSerialPortInstructions.REQ_RFIDWriteDeviceUIDAndSequenceID ,16},
            { (int)ControllerSerialPortInstructions.REPORT_RFIDDeviceUIDAndSequenceID ,17},
            { (int)ControllerSerialPortInstructions.RESP_RFIDReadDeviceUIDAndSequenceID ,17},
            { (int)ControllerSerialPortInstructions.RESP_RFIDWriteDeviceUIDAndSequenceID ,1},

            { (int)ControllerSerialPortInstructions.REQ_SetControlBroadCastBaudRate ,1},
            { (int)ControllerSerialPortInstructions.REQ_GetControlBroadCastBaudRate ,0},
            { (int)ControllerSerialPortInstructions.RESP_GetControlBroadCastBaudRate ,1},
            { (int)ControllerSerialPortInstructions.RESP_SetControlBroadCastBaudRate ,1},
        };

        public static Dictionary<int, int> CFG_DSPI = new Dictionary<int, int> {
            { (int)DeviceSerialPortInstructions.REQ_GetUUID ,0 },// 不成结构
            { (int)DeviceSerialPortInstructions.REQ_PlaySmell ,2},
            { (int)DeviceSerialPortInstructions.REQ_SetPhysicalChannel ,1},
            { (int)DeviceSerialPortInstructions.REQ_GetPhysicalChannel ,0},
            { (int)DeviceSerialPortInstructions.REQ_SetFactoryNumber ,4},

            { (int)DeviceSerialPortInstructions.RESP_SetFactoryNumber ,1},
            { (int)DeviceSerialPortInstructions.RESP_GetUUID ,12},
            { (int)DeviceSerialPortInstructions.RESP_PlaySmell ,1},
            { (int)DeviceSerialPortInstructions.RESP_SetPhysicalChannel ,1},
            { (int)DeviceSerialPortInstructions.RESP_GetPhysicalChannel ,1},
        };

        public static Dictionary<int, int> CFG_DB4AI = new Dictionary<int, int> {
            { (int)DeviceBroadcast4AddrInstructions.REQ_AssginDevAsMainFrame ,-2 },// -2 表示动态获取
            { (int)DeviceBroadcast4AddrInstructions.REC_DevReportFlowOver ,-2 },
            { (int)DeviceBroadcast4AddrInstructions.REC_WhetherStop ,0 },
            { (int)DeviceBroadcast4AddrInstructions.REQ_DeviceQueuyStatus ,0 },



            { (int)DeviceBroadcast4AddrInstructions.RESP_DeviceQueuyStatus ,1 },

            { (int)DeviceBroadcast4AddrInstructions.RESP_DevReportFlowOver ,0 },
            { (int)DeviceBroadcast4AddrInstructions.RESP_WhetherStop ,1 },
            { (int)DeviceBroadcast4AddrInstructions.RESP_AssginDevAsMainFrame ,1},
        };

        public static Dictionary<int, int> CFG_CSCI = new Dictionary<int, int> {
            { (int)ChestSocketCommInstructions.REQ_UDPBroadCastAddressing ,0 },
            { (int)ChestSocketCommInstructions.REQ_GetDevicesInPhysicalChannel ,3},
            { (int)ChestSocketCommInstructions.REQ_CloseSocket ,1},


            { (int)ChestSocketCommInstructions.RESP_UDPBroadCastAddressing ,0},
            { (int)ChestSocketCommInstructions.RESP_GetDevicesInPhysicalChannelStart ,3},
            { (int)ChestSocketCommInstructions.RESP_GetDevicesInPhysicalChannelItems ,17},
            { (int)ChestSocketCommInstructions.RESP_GetDevicesInPhysicalChannelEnd ,0},
            { (int)ChestSocketCommInstructions.RESP_CloseSocket ,0},
        };
        public static Dictionary<int, int> CFG_ADSPI = new Dictionary<int, int> {
            { (int)ADPlayerSerialPortInstruction.REQ_PlaySmell ,-2 },
            { (int)ADPlayerSerialPortInstruction.REQ_StopPlaySmell ,-2},
            { (int)ADPlayerSerialPortInstruction.RESP_PlaySmell ,-2},
            { (int)ADPlayerSerialPortInstruction.RESP_StopPlaySmell ,-2},
        };

        // ADPlayerSerialPortInstruction

        public static Dictionary<Type, Dictionary<int, int>> CFGMap = new Dictionary<Type, Dictionary<int, int>>() {
            { typeof(DeviceBroadcastInstructions),CFG_DBI},
            { typeof(ControllerSerialPortInstructions),CFG_CSPI},
            { typeof(DeviceSerialPortInstructions),CFG_DSPI},
            { typeof(ChestSocketCommInstructions),CFG_CSCI},
            { typeof(DeviceBroadcast4AddrInstructions),CFG_DB4AI},

            { typeof(ADPlayerSerialPortInstruction),CFG_ADSPI},
        };

        public static Dictionary<int, int> GetInstructionTypeCFG(Type type)
        {
            if (CFGMap.ContainsKey(type))
            {
                return CFGMap[type];
            }
            return null;
        }
    }

    public enum DataPackageType : byte
    {
        DPT_TransparentTransmissionBroadcast = 0x00,// 透传广播
        DPT_ControllerSerialPort = 0x01,// 遥控器串口
        DPT_DeviceSerialPort = 0x01,// 设备串口
    }

    /// <summary>
    /// 设备类型
    /// </summary>
    public enum PackageDeviceType : Byte
    {
        PDT_HandheldRemoteControlUnit = 0x01, // 手持遥控器
        PDT_CinemaSmellPlayer = 0x02,         // 影院气味播放器


        PDT_ChestDevice = 0x03, // 机柜设备
        PDT_UpperComputer = 0x04,         // 与机柜设备交互的上位机
    }

    /// <summary>
    /// 设备串口指令集
    /// </summary>
    public enum DeviceSerialPortInstructions : Byte
    {
        REQ_GetUUID = 0x01, // 获取 UUID
        REQ_PlaySmell = 0x02,// 播放气味
        REQ_SetPhysicalChannel = 0x05,// 设置物理信道 
        REQ_GetPhysicalChannel = 0x06,// 获取物理信道

        REQ_SetFactoryNumber = 0x0b,// 获取物理信道

        RESP_GetUUID = 0x81,
        RESP_PlaySmell = 0x82,
        RESP_SetPhysicalChannel = 0x85,
        RESP_GetPhysicalChannel = 0x86,
        RESP_SetFactoryNumber = 0x8b,
    }


    /// <summary>
    /// 设备串口指令集
    /// </summary>
    public enum ChestSocketCommInstructions : Byte
    {
        REQ_UDPBroadCastAddressing = 0x01, // UDP广播寻址
        REQ_GetDevicesInPhysicalChannel = 0x02,// 获取某个物理信道下的设备列表
        REQ_CloseSocket = 0x21,// 关闭Socket


        RESP_UDPBroadCastAddressing = 0x81,// UDP广播寻址 回复
        RESP_GetDevicesInPhysicalChannelStart = 0x91,// 某个物理信道下的设备列表 总览
        RESP_GetDevicesInPhysicalChannelItems = 0x92,// 某个物理信道下的设备列表 每条
        RESP_GetDevicesInPhysicalChannelEnd = 0x93,// 某个物理信道下的设备列表 结束

        RESP_CloseSocket = 0xa1,// 关闭Socket
    }


    /// <summary>
    /// 透传广播指令集
    /// </summary>
    public enum ControllerSerialPortInstructions : Byte
    {
        REQ_AssignPhysicalChannel = 0x25, //  分配设备物理信道
        REQ_AssignControllerPhysicalChannel = 0x26, //  设置遥控器物理信道
        REQ_GetControllerPhysicalChannel = 0x27, //  获取物理信道
        REQ_GetFirmwareVerison = 0x28,// 遥控器固件版本

        REQ_ReadControllerChannel = 0x34,   // 读取遥控器信道
        REQ_TransparentTransmission = 0x51, // 透传


        REQ_DeviceSyncScript = 0x0f, //  4.36 [遥控器无线]指定的主机同步设备（0x0F）

        REQ_RFIDReadDeviceUIDAndSequenceID = 0x61, // 主动读取UID
        REQ_RFIDWriteDeviceUIDAndSequenceID = 0x62, // 主动写入UID、编号
        REPORT_RFIDDeviceUIDAndSequenceID = 0xe0, // 自动上报设备UID编号

        REQ_SetControlBroadCastBaudRate = 0x09,// 设置广播通信速率
        REQ_GetControlBroadCastBaudRate = 0x0a,// 读取广播通信速率

        RESP_GetControlBroadCastBaudRate = 0x8a,// 读取广播通信速率
        RESP_SetControlBroadCastBaudRate = 0x89,// 设置广播通信速率

        RESP_AssignControllerPhysicalChannel = 0xa6, //  设置遥控器物理信道
        RESP_GetControllerPhysicalChannel = 0xa7, //  获取物理信道
        RESP_GetFirmwareVerison = 0xa8,// 遥控器固件版本

        RESP_ReadControllerChannel = 0xb4,
        RESP_TransparentTransmission = 0xd1, // 透传

        RESP_RFIDReadDeviceUIDAndSequenceID = 0xe1, // 主动读取UID
        RESP_RFIDWriteDeviceUIDAndSequenceID = 0xe2, // 主动写入UID、编号
    }

    /// <summary>
    /// 透传广播指令集
    /// </summary>
    public enum DeviceBroadcastInstructions : Byte
    {
        E_ERROR = 0x00,       //  错误

        // REQ
        REQ_PlaySmell = 0x01,       //  播放气味
        REQ_StopPlay = 0x02,        //  停止播放
        REQ_PLayScript = 0x03,      //  播放脚本
        REQ_StopScript = 0x04,      //  停止脚本
        REQ_GetFirmwareVerison = 0x05,      //  获取固件信息

        REQ_SetDeviceBroadCastBaudRate = 0x09,// 设置设备的广播通信速率

        REQ_ScriptLoadStart = 0x11, //  开始下载脚本
        REQ_ScriptLoading = 0x12,   //  下载脚本
        REQ_ScriptLoaded = 0x13,    //  脚本下载完成
        REQ_ScriptQuery = 0x14,     //  脚本查询

        REQ_StatusQuery = 0x21,     //  设备状态查询

        REQ_StatusQueryAll = 0x31,     //  设备所有状态查询，状态，脚本、字幕



        REQ_SetChannel = 0x22,      //  设置设备通信信道
        REQ_AssignAddress = 0x24,      //  分配地址

        REQ_UpgradeFileLoadStart = 0x26, //  开始下载升级程序
        REQ_UpgradeFileLoading = 0x27,   //  下载升级程序
        REQ_UpgradeFileLoaded = 0x28,    //  升级程序下载完成

        REQ_FontLoadStart = 0x29, //  开始下载字库
        REQ_FontLoadStartMD5 = 0x30, //  开始下载字库

        REQ_FontLoading = 0x2a,   //  下载字库
        REQ_FontLoaded = 0x2b,    //  字库下载完成

        REQ_SmellNameMapStart = 0x2c, //  开始解析字库字库
        REQ_SmellNameMapLoading = 0x2d,   //  解析字库
        REQ_SmellNameMapLoaded = 0x2e,    //  字库解析完成

        REQ_SetDevicePhysicalChannel = 0x51, // 设置物理信道

        RESP_SetDevicePhysicalChannel = 0xD1,

        // RESP
        RESP_GetFirmwareVerison = 0x85,
        RESP_ScriptQuery = 0x94,
        RESP_StatusQuery = 0xa1,
        RESP_SetChannel = 0xa2,
        RESP_AssignAddress = 0xa4,

        RESP_StatusQueryAll = 0xb1,
    }

    public enum DeviceBroadcast4AddrInstructions : Byte
    {
        REQ_AssginDevAsMainFrame = 0x0a,// 【自组网】指定设备为主机

        REQ_DeviceQueuyStatus = 0x0b,// 【自组网】4.12 [设备无线]设备主机查询设备状态0x0B
        REC_WhetherStop = 0x0c,  // 【自组网】主机向遥控器请求是否终止流程
        REC_DevReportFlowOver = 0x0d,  // 【自组网】设备主动发送结束指令到主机(0x0D)


        RESP_DeviceQueuyStatus = 0x8b,

        RESP_DevReportFlowOver = 0x8d,
        RESP_WhetherStop = 0x8c,
        RESP_AssginDevAsMainFrame = 0x8a,
    }

    public enum ADPlayerSerialPortInstruction : Byte
    {
        REQ_PlaySmell = 0x01,
        REQ_StopPlaySmell = 0x02,
        RESP_StopPlaySmell = 0x82,
        RESP_PlaySmell = 0x81,
    }


    public enum RF100SerialPortInstructions : Byte
    {
        ERROR = 0xff,


        REQ_ReadWriterInfo = 0x03, // 获取读写器模块信息

        REQ_SingleInventory = 0x22, // 单次轮询指令
        REQ_StartMultiInventory = 0x27, // 开始多次轮询指令
        REQ_StopMultiInventory = 0x28, // 结束多次轮询指令


        REQ_ReadMemBank = 0x39, // 读标签数据存储区
        REQ_WriteMemBank = 0x49, // 写标签数据存储区
    }
}
