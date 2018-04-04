
Imports System.IO
Imports System.Collections
Imports System.Runtime.InteropServices
Imports System.Text


Namespace DriveInfo
    ''' <summary>
    ''' 
    ''' </summary>
    Public Structure enCdRomInfo
        Public strDriveType As String, strLabel As String, strSn As String, strFileSystem As String
        Public fCapacity As String, fSpace As String


    End Structure

    Public Class ClsDriveInfo
        Const DRIVE_CDROM As Integer = 5
        Const DRIVE_FIXED As Integer = 3
        Const DRIVE_NO_ROOT As Integer = 1
        Const DRIVE_REMOTE As Integer = 4
        Const DRIVE_REMOVABLE As Integer = 2

        Public Sub New()
        End Sub

        <DllImport("kernel32")> _
        Private Shared Function GetDriveType(ByVal path As String) As Integer
        End Function
        <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Private Shared Function GetVolumeInformation(ByVal rootPathName As String, <Out()> ByVal volNameBuffer As StringBuilder, ByVal volNameSize As Integer, ByRef volSerialNumber As Integer, ByRef maxComponentLength As Integer, ByRef fileSysFlags As Integer, _
   <Out()> ByVal fileSystemName As StringBuilder, ByVal fileSystemNameSize As Integer) As Boolean
        End Function

        <DllImport("kernel32")> _
        Private Shared Function GetDiskFreeSpaceEx(ByVal path As String, ByRef lpFreeAvail As Int64, ByRef lpCapacity As Int64, ByRef lpFree As Int64) As Boolean
        End Function


        Private Shared Function ConvertBytes(ByVal nValue As Single) As String
            Dim strValue As String = ""

            If nValue > 1000000000 Then
                nValue = nValue / (1024 * 1024 * 1024)
                nValue = CSng(Math.Round(CDbl(nValue), 2))
                strValue = nValue.ToString() + " " + "GB"
            ElseIf nValue >= 1000000 AndAlso nValue <= 1000000000 Then
                nValue = nValue / (1024 * 1024)
                nValue = CSng(Math.Round(CDbl(nValue), 2))

                strValue = nValue.ToString() + " " + "MB"
            ElseIf nValue >= 1000 AndAlso nValue < 1000000 Then
                nValue = nValue / (1024)
                nValue = CSng(Math.Round(CDbl(nValue), 2))
                strValue = nValue.ToString() + " " + "KB"
            ElseIf nValue < 1000 Then
                nValue = CSng(Math.Round(CDbl(nValue), 2))
                strValue = nValue.ToString() + " " + "bytes"
            End If
            Return strValue
        End Function

        Public Shared Function EnumDrives() As String()
            Dim strDrives As String()

            strDrives = Directory.GetLogicalDrives()
            Return strDrives
        End Function

        Public Shared Function GetDriveInfo(ByVal strDrive As String) As enCdRomInfo

            Dim enInfo As New enCdRomInfo()
            Dim inf As New DirectoryInfo(strDrive)
            enInfo.strLabel = ""
            enInfo.strFileSystem = ""
            enInfo.strSn = ""
            enInfo.fCapacity = ""
            enInfo.fSpace = ""

            Dim volName As New StringBuilder(260)
            Dim fileSysName As New StringBuilder(260)
            Dim volSer As Integer, maxCmp As Integer, sysFlgs As Integer, err As Integer, driveType As Integer
            Dim nTotal As Int64, nFree As Int64, nAvail As Int64

            driveType = GetDriveType(strDrive)
            Select Case driveType
                Case DRIVE_CDROM
                    enInfo.strDriveType = "CD-ROM"
                    Exit Select
                Case DRIVE_FIXED
                    enInfo.strDriveType = "HDD"
                    Exit Select
                Case DRIVE_REMOTE
                    enInfo.strDriveType = "Network Drive"
                    Exit Select
                Case DRIVE_REMOVABLE
                    enInfo.strDriveType = "Removable"
                    Exit Select
                Case Else
                    enInfo.strDriveType = "HDD"
                    Exit Select
            End Select
            If inf.Exists Then
                Dim ok As Boolean = GetVolumeInformation(strDrive, volName, volName.Capacity, volSer, maxCmp, sysFlgs, _
                 fileSysName, fileSysName.Capacity)
                If Not ok Then
                    err = Marshal.GetLastWin32Error()
                End If
                ok = GetDiskFreeSpaceEx(strDrive, nAvail, nTotal, nFree)
                If Not ok Then
                    err = Marshal.GetLastWin32Error()
                End If

                enInfo.strLabel = volName.ToString()
                enInfo.strFileSystem = fileSysName.ToString()
                enInfo.strSn = volSer.ToString()
                enInfo.fCapacity = ConvertBytes(nTotal)
                enInfo.fSpace = ConvertBytes(nFree)
                enInfo.strFileSystem = fileSysName.ToString()
            Else
                enInfo.strLabel = (Convert.ToString(enInfo.strDriveType & Convert.ToString(" ")) & strDrive) + " not present"
            End If
            Return enInfo

        End Function

    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
