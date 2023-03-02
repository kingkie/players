#!/bin/sh
Version="v2.1.0-JD.`date +%Y%m%d%H%I%M`"
filename="BroadcastPlayer-"$Version".tar" 

cd ../bin/Debug
mkdir Libs
cp NPOI.dll                                    \
NPOI.OOXML.dll                              \
NPOI.OpenXml4Net.dll                        \
NPOI.OpenXmlFormats.dll                     \
System.Data.SQLite.dll                      \
AxInterop.WMPLib.dll                        \
Interop.WMPLib.dll                          \
ScentrealmBCC.dll                        \
./Libs
tar -cvf $filename                   \
x64                                         \
x86                                         \
BroadcastPlayer.exe                         \
BroadcastPlayer.exe.config                  \
Libs                                        \
USM                                         
mv $filename /d/desktop/ScentrealmExe/BroadcastPlayer/
rm -fr Libs