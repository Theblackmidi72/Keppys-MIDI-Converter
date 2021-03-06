﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.AddOn.Vst;
using Un4seen.Bass.AddOn.Midi;
using System.Globalization;
using System.Resources;

namespace KeppyMIDIConverter
{
    public partial class VSTManagerWindow : Form
    {
        public VSTManagerWindow()
        {
            InitializeComponent();
        }

        public static string bitnow = null;
        public static string bitreq = null;
        ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        CultureInfo cul;            // declare culture info

        private void InitializeLanguage()
        {
            Text = MainWindow.res_man.GetString("VSTManager", MainWindow.cul);
            label1.Text = MainWindow.res_man.GetString("EmptySlot", MainWindow.cul) + " 1";
            label2.Text = MainWindow.res_man.GetString("EmptySlot", MainWindow.cul) + " 2";
            label3.Text = MainWindow.res_man.GetString("EmptySlot", MainWindow.cul) + " 3";
            label4.Text = MainWindow.res_man.GetString("EmptySlot", MainWindow.cul) + " 4";
            label5.Text = MainWindow.res_man.GetString("EmptySlot", MainWindow.cul) + " 5";
            label6.Text = MainWindow.res_man.GetString("EmptySlot", MainWindow.cul) + " 6";
            label7.Text = MainWindow.res_man.GetString("EmptySlot", MainWindow.cul) + " 7";
            label8.Text = MainWindow.res_man.GetString("EmptySlot", MainWindow.cul) + " 8";
            Load1.Text = MainWindow.res_man.GetString("LoadVST", MainWindow.cul);
            Load2.Text = MainWindow.res_man.GetString("LoadVST", MainWindow.cul);
            Load3.Text = MainWindow.res_man.GetString("LoadVST", MainWindow.cul);
            Load4.Text = MainWindow.res_man.GetString("LoadVST", MainWindow.cul);
            Load5.Text = MainWindow.res_man.GetString("LoadVST", MainWindow.cul);
            Load6.Text = MainWindow.res_man.GetString("LoadVST", MainWindow.cul);
            Load7.Text = MainWindow.res_man.GetString("LoadVST", MainWindow.cul);
            Load8.Text = MainWindow.res_man.GetString("LoadVST", MainWindow.cul);
            Unload.Text = MainWindow.res_man.GetString("UnloadVST", MainWindow.cul);
            Unload2.Text = MainWindow.res_man.GetString("UnloadVST", MainWindow.cul);
            Unload3.Text = MainWindow.res_man.GetString("UnloadVST", MainWindow.cul);
            Unload4.Text = MainWindow.res_man.GetString("UnloadVST", MainWindow.cul);
            Unload5.Text = MainWindow.res_man.GetString("UnloadVST", MainWindow.cul);
            Unload6.Text = MainWindow.res_man.GetString("UnloadVST", MainWindow.cul);
            Unload7.Text = MainWindow.res_man.GetString("UnloadVST", MainWindow.cul);
            Unload8.Text = MainWindow.res_man.GetString("UnloadVST", MainWindow.cul);
            UnloadAllVSTs.Text = MainWindow.res_man.GetString("UnloadAllVSTs", MainWindow.cul);
            Desc.Text = MainWindow.res_man.GetString("VSTHowDoTheyWork", MainWindow.cul);
        }

        private void VSTManagerWindow_Load(object sender, EventArgs e)
        {
            Size = new System.Drawing.Size(633, 468);
            InitializeLanguage();
            if (IntPtr.Size == 8)
            {
                bitnow = "64-bit";
                bitreq = "32-bit";
            }
            else if (IntPtr.Size == 4)
            {
                bitnow = "32-bit";
                bitreq = "64-bit";
            }
            if (KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc != null)
            {
                label1.Text = KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc;
                Unload.Enabled = true;
                Load1.Enabled = false;
            }
            if (KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc2 != null)
            {
                label2.Text = KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc2;
                Unload2.Enabled = true;
                Load2.Enabled = false;
            }
            if (KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc3 != null)
            {
                label3.Text = KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc3;
                Unload3.Enabled = true;
                Load3.Enabled = false;
            }
            if (KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc4 != null)
            {
                label4.Text = KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc4;
                Unload4.Enabled = true;
                Load4.Enabled = false;
            }
            if (KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc5 != null)
            {
                label5.Text = KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc5;
                Unload5.Enabled = true;
                Load5.Enabled = false;
            }
            if (KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc6 != null)
            {
                label6.Text = KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc6;
                Unload6.Enabled = true;
                Load6.Enabled = false;
            }
            if (KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc7 != null)
            {
                label1.Text = KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc7;
                Unload7.Enabled = true;
                Load7.Enabled = false;
            }
            if (KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc8 != null)
            {
                label8.Text = KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc8;
                Unload8.Enabled = true;
                Load8.Enabled = false;
            }
            if (MainWindow.KMCGlobals.IsLoudMaxEnabled)
            {
                LoudMaxCheck.ForeColor = Color.Green;
                LoudMaxCheck.Text = "LoudMax on";
                LoudMaxCheck.Checked = true;
            }
            else
            {
                LoudMaxCheck.ForeColor = Color.DarkRed;
                LoudMaxCheck.Text = "LoudMax off";
                LoudMaxCheck.Checked = false;
            }
        }

        private void InitStartDirectory()
        {
            Registry.CurrentUser.CreateSubKey("SOFTWARE\\Keppy's MIDI Converter\\Settings");
            RegistryKey Settings = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's MIDI Converter\\Settings", true);
            try
            {
                VSTImportDialog.InitialDirectory = Settings.GetValue("lastvstfolder", System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).ToString();
            }
            catch
            {
                Settings.Close();
            }
            Settings.Close();
        }

        private void SaveDirectory(string file)
        {
            Registry.CurrentUser.CreateSubKey("SOFTWARE\\Keppy's MIDI Converter\\Settings");
            RegistryKey Settings = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's MIDI Converter\\Settings", true);
            try
            {
                Settings.SetValue("lastvstfolder", Path.GetDirectoryName(file), RegistryValueKind.String);
                Settings.Close();
            }
            catch
            {
                Settings.Close();
            }
        }

        private void Load_Click(object sender, EventArgs e)
        {
            InitStartDirectory();
            if (VSTImportDialog.ShowDialog() == DialogResult.OK)
            {
                Un4seen.Bass.Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                int Test = Bass.BASS_StreamCreateDummy(44100, 2, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
                int VSTTester = BassVst.BASS_VST_ChannelSetDSP(Test, VSTImportDialog.FileName, BASSVSTDsp.BASS_VST_DEFAULT, 1);
                BASS_VST_INFO vstInfo = new BASS_VST_INFO();
                if (ModifierKeys == Keys.Shift)
                {
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL = VSTImportDialog.FileName;
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc = VSTImportDialog.FileName + " (Not verified)";
                    label1.Text = VSTImportDialog.FileName + " (Not verified)";
                    Bass.BASS_Free();
                    Unload.Enabled = true;
                    Load1.Enabled = false;
                    SaveDirectory(VSTImportDialog.FileName);
                }
                else
                {
                    if (BassVst.BASS_VST_GetInfo(VSTTester, vstInfo))
                    {
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL = VSTImportDialog.FileName;
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        label1.Text = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        Bass.BASS_Free();
                        Unload.Enabled = true;
                        Load1.Enabled = false;
                        SaveDirectory(VSTImportDialog.FileName);
                    }
                    else
                    {
                        Bass.BASS_Free();
                        MessageBox.Show(String.Format(MainWindow.res_man.GetString("InvalidVSTLoaded", cul), Path.GetFileNameWithoutExtension(VSTImportDialog.FileName), bitreq, bitnow), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }  
        }

        private void Load2_Click(object sender, EventArgs e)
        {
            InitStartDirectory();
            if (VSTImportDialog.ShowDialog() == DialogResult.OK)
            {
                Un4seen.Bass.Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                int Test = Bass.BASS_StreamCreateDummy(44100, 2, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
                int VSTTester = BassVst.BASS_VST_ChannelSetDSP(Test, VSTImportDialog.FileName, BASSVSTDsp.BASS_VST_DEFAULT, 1);
                BASS_VST_INFO vstInfo = new BASS_VST_INFO();
                if (ModifierKeys == Keys.Shift)
                {
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL2 = VSTImportDialog.FileName;
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc2 = VSTImportDialog.FileName + " (Not verified)";
                    label2.Text = VSTImportDialog.FileName + " (Not verified)";
                    Bass.BASS_Free();
                    Unload2.Enabled = true;
                    Load2.Enabled = false;
                    SaveDirectory(VSTImportDialog.FileName);
                }
                else
                {
                    if (BassVst.BASS_VST_GetInfo(VSTTester, vstInfo))
                    {
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL2 = VSTImportDialog.FileName;
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc2 = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        label2.Text = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        Bass.BASS_Free();
                        Unload2.Enabled = true;
                        Load2.Enabled = false;
                        SaveDirectory(VSTImportDialog.FileName);
                    }
                    else
                    {
                        Bass.BASS_Free();
                        MessageBox.Show(String.Format(MainWindow.res_man.GetString("InvalidVSTLoaded", cul), Path.GetFileNameWithoutExtension(VSTImportDialog.FileName), bitreq, bitnow), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }  
        }

        private void Load3_Click(object sender, EventArgs e)
        {
            InitStartDirectory();
            if (VSTImportDialog.ShowDialog() == DialogResult.OK)
            {
                Un4seen.Bass.Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                int Test = Bass.BASS_StreamCreateDummy(44100, 2, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
                int VSTTester = BassVst.BASS_VST_ChannelSetDSP(Test, VSTImportDialog.FileName, BASSVSTDsp.BASS_VST_DEFAULT, 1);
                BASS_VST_INFO vstInfo = new BASS_VST_INFO();
                if (ModifierKeys == Keys.Shift)
                {
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL3 = VSTImportDialog.FileName;
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc3 = VSTImportDialog.FileName + " (Not verified)";
                    label3.Text = VSTImportDialog.FileName + " (Not verified)";
                    Bass.BASS_Free();
                    Unload3.Enabled = true;
                    Load3.Enabled = false;
                    SaveDirectory(VSTImportDialog.FileName);
                }
                else
                {
                    if (BassVst.BASS_VST_GetInfo(VSTTester, vstInfo))
                    {
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL3 = VSTImportDialog.FileName;
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc3 = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        label3.Text = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        Bass.BASS_Free();
                        Unload3.Enabled = true;
                        Load3.Enabled = false;
                        SaveDirectory(VSTImportDialog.FileName);
                    }
                    else
                    {
                        Bass.BASS_Free();
                        MessageBox.Show(String.Format(MainWindow.res_man.GetString("InvalidVSTLoaded", cul), Path.GetFileNameWithoutExtension(VSTImportDialog.FileName), bitreq, bitnow), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }  
        }

        private void Load4_Click(object sender, EventArgs e)
        {
            InitStartDirectory();
            if (VSTImportDialog.ShowDialog() == DialogResult.OK)
            {
                Un4seen.Bass.Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                int Test = Bass.BASS_StreamCreateDummy(44100, 2, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
                int VSTTester = BassVst.BASS_VST_ChannelSetDSP(Test, VSTImportDialog.FileName, BASSVSTDsp.BASS_VST_DEFAULT, 1);
                BASS_VST_INFO vstInfo = new BASS_VST_INFO();
                if (ModifierKeys == Keys.Shift)
                {
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL4 = VSTImportDialog.FileName;
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc4 = VSTImportDialog.FileName + " (Not verified)";
                    label4.Text = VSTImportDialog.FileName + " (Not verified)";
                    Bass.BASS_Free();
                    Unload4.Enabled = true;
                    Load4.Enabled = false;
                    SaveDirectory(VSTImportDialog.FileName);
                }
                else
                {
                    if (BassVst.BASS_VST_GetInfo(VSTTester, vstInfo))
                    {
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL4 = VSTImportDialog.FileName;
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc4 = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        label4.Text = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        Bass.BASS_Free();
                        Unload4.Enabled = true;
                        Load4.Enabled = false;
                        SaveDirectory(VSTImportDialog.FileName);
                    }
                    else
                    {
                        Bass.BASS_Free();
                        MessageBox.Show(String.Format(MainWindow.res_man.GetString("InvalidVSTLoaded", cul), Path.GetFileNameWithoutExtension(VSTImportDialog.FileName), bitreq, bitnow), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }  
        }

        private void Load5_Click(object sender, EventArgs e)
        {
            InitStartDirectory();
            if (VSTImportDialog.ShowDialog() == DialogResult.OK)
            {
                Un4seen.Bass.Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                int Test = Bass.BASS_StreamCreateDummy(44100, 2, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
                int VSTTester = BassVst.BASS_VST_ChannelSetDSP(Test, VSTImportDialog.FileName, BASSVSTDsp.BASS_VST_DEFAULT, 1);
                BASS_VST_INFO vstInfo = new BASS_VST_INFO();
                if (ModifierKeys == Keys.Shift)
                {
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL5 = VSTImportDialog.FileName;
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc5 = VSTImportDialog.FileName + " (Not verified)";
                    label1.Text = VSTImportDialog.FileName + " (Not verified)";
                    Bass.BASS_Free();
                    Unload5.Enabled = true;
                    Load5.Enabled = false;
                    SaveDirectory(VSTImportDialog.FileName);
                }
                else
                {
                    if (BassVst.BASS_VST_GetInfo(VSTTester, vstInfo))
                    {
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL5 = VSTImportDialog.FileName;
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc5 = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        label5.Text = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        Bass.BASS_Free();
                        Unload5.Enabled = true;
                        Load5.Enabled = false;
                        SaveDirectory(VSTImportDialog.FileName);
                    }
                    else
                    {
                        Bass.BASS_Free();
                        MessageBox.Show(String.Format(MainWindow.res_man.GetString("InvalidVSTLoaded", cul), Path.GetFileNameWithoutExtension(VSTImportDialog.FileName), bitreq, bitnow), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }  
        }

        private void Load6_Click(object sender, EventArgs e)
        {
            InitStartDirectory();
            if (VSTImportDialog.ShowDialog() == DialogResult.OK)
            {
                Un4seen.Bass.Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                int Test = Bass.BASS_StreamCreateDummy(44100, 2, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
                int VSTTester = BassVst.BASS_VST_ChannelSetDSP(Test, VSTImportDialog.FileName, BASSVSTDsp.BASS_VST_DEFAULT, 1);
                BASS_VST_INFO vstInfo = new BASS_VST_INFO();
                if (ModifierKeys == Keys.Shift)
                {
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL6 = VSTImportDialog.FileName;
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc6 = VSTImportDialog.FileName + " (Not verified)";
                    label6.Text = VSTImportDialog.FileName + " (Not verified)";
                    Bass.BASS_Free();
                    Unload6.Enabled = true;
                    Load6.Enabled = false;
                    SaveDirectory(VSTImportDialog.FileName);
                }
                else
                {
                    if (BassVst.BASS_VST_GetInfo(VSTTester, vstInfo))
                    {
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL6 = VSTImportDialog.FileName;
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc6 = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        label6.Text = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        Bass.BASS_Free();
                        Unload6.Enabled = true;
                        Load6.Enabled = false;
                        SaveDirectory(VSTImportDialog.FileName);
                    }
                    else
                    {
                        Bass.BASS_Free();
                        MessageBox.Show(String.Format(MainWindow.res_man.GetString("InvalidVSTLoaded", cul), Path.GetFileNameWithoutExtension(VSTImportDialog.FileName), bitreq, bitnow), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }  
        }

        private void Load7_Click(object sender, EventArgs e)
        {
            InitStartDirectory();
            if (VSTImportDialog.ShowDialog() == DialogResult.OK)
            {
                Un4seen.Bass.Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                int Test = Bass.BASS_StreamCreateDummy(44100, 2, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
                int VSTTester = BassVst.BASS_VST_ChannelSetDSP(Test, VSTImportDialog.FileName, BASSVSTDsp.BASS_VST_DEFAULT, 1);
                BASS_VST_INFO vstInfo = new BASS_VST_INFO();
                if (ModifierKeys == Keys.Shift)
                {
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL7 = VSTImportDialog.FileName;
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc7 = VSTImportDialog.FileName + " (Not verified)";
                    label7.Text = VSTImportDialog.FileName + " (Not verified)";
                    Bass.BASS_Free();
                    Unload7.Enabled = true;
                    Load7.Enabled = false;
                    SaveDirectory(VSTImportDialog.FileName);
                }
                else
                {
                    if (BassVst.BASS_VST_GetInfo(VSTTester, vstInfo))
                    {
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL7 = VSTImportDialog.FileName;
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc7 = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        label7.Text = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        Bass.BASS_Free();
                        Unload7.Enabled = true;
                        Load7.Enabled = false;
                        SaveDirectory(VSTImportDialog.FileName);
                    }
                    else
                    {
                        Bass.BASS_Free();
                        MessageBox.Show(String.Format(MainWindow.res_man.GetString("InvalidVSTLoaded", cul), Path.GetFileNameWithoutExtension(VSTImportDialog.FileName), bitreq, bitnow), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }  
        }

        private void Load8_Click(object sender, EventArgs e)
        {
            InitStartDirectory();
            if (VSTImportDialog.ShowDialog() == DialogResult.OK)
            {
                Un4seen.Bass.Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                int Test = Bass.BASS_StreamCreateDummy(44100, 2, BASSFlag.BASS_STREAM_DECODE, IntPtr.Zero);
                int VSTTester = BassVst.BASS_VST_ChannelSetDSP(Test, VSTImportDialog.FileName, BASSVSTDsp.BASS_VST_DEFAULT, 1);
                BASS_VST_INFO vstInfo = new BASS_VST_INFO();
                if (ModifierKeys == Keys.Shift)
                {
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL8 = VSTImportDialog.FileName;
                    KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc8 = VSTImportDialog.FileName + " (Not verified)";
                    label8.Text = VSTImportDialog.FileName + " (Not verified)";
                    Bass.BASS_Free();
                    Unload8.Enabled = true;
                    Load8.Enabled = false;
                    SaveDirectory(VSTImportDialog.FileName);
                }
                else
                {
                    if (BassVst.BASS_VST_GetInfo(VSTTester, vstInfo))
                    {
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL8 = VSTImportDialog.FileName;
                        KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc8 = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        label8.Text = vstInfo.productName + " by " + vstInfo.vendorName + " (Version: " + vstInfo.vendorVersion + ")";
                        Bass.BASS_Free();
                        Unload8.Enabled = true;
                        Load8.Enabled = false;
                        SaveDirectory(VSTImportDialog.FileName);
                    }
                    else
                    {
                        Bass.BASS_Free();
                        MessageBox.Show(String.Format(MainWindow.res_man.GetString("InvalidVSTLoaded", cul), Path.GetFileNameWithoutExtension(VSTImportDialog.FileName), bitreq, bitnow), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }  
        }

        private void Unload_Click(object sender, EventArgs e)
        {
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL = null;
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc = null;
            label1.Text = res_man.GetString("EmptySlot", cul) + " 1";
            Unload.Enabled = false;
            Load1.Enabled = true;
        }

        private void Unload2_Click(object sender, EventArgs e)
        {
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL2 = null;
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc2 = null;
            label2.Text = res_man.GetString("EmptySlot", cul) + " 2";
            Unload2.Enabled = false;
            Load2.Enabled = true;
        }

        private void Unload3_Click(object sender, EventArgs e)
        {
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL3 = null;
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc3 = null;
            label3.Text = res_man.GetString("EmptySlot", cul) + " 3";
            Unload3.Enabled = false;
            Load3.Enabled = true;
        }

        private void Unload4_Click(object sender, EventArgs e)
        {
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL4 = null;
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc4 = null;
            label4.Text = res_man.GetString("EmptySlot", cul) + " 4";
            Unload4.Enabled = false;
            Load4.Enabled = true;
        }

        private void Unload5_Click(object sender, EventArgs e)
        {
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL5 = null;
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc5 = null;
            label5.Text = res_man.GetString("EmptySlot", cul) + " 5";
            Unload5.Enabled = false;
            Load5.Enabled = true;
        }

        private void Unload6_Click(object sender, EventArgs e)
        {
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL6 = null;
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc6 = null;
            label6.Text = res_man.GetString("EmptySlot", cul) + " 6";
            Unload6.Enabled = false;
            Load6.Enabled = true;
        }

        private void Unload7_Click(object sender, EventArgs e)
        {
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL7 = null;
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc7 = null;
            label7.Text = res_man.GetString("EmptySlot", cul) + " 7";
            Unload7.Enabled = false;
            Load7.Enabled = true;
        }

        private void Unload8_Click(object sender, EventArgs e)
        {
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLL8 = null;
            KeppyMIDIConverter.MainWindow.KMCGlobals.VSTDLLDesc8 = null;
            label8.Text = res_man.GetString("EmptySlot", cul) + " 8";
            Unload8.Enabled = false;
            Load8.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Unload.PerformClick();
            Unload2.PerformClick();
            Unload3.PerformClick();
            Unload4.PerformClick();
            Unload5.PerformClick();
            Unload6.PerformClick();
            Unload7.PerformClick();
            Unload8.PerformClick();
        }

        private void LoudMaxCheck_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey Settings = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's MIDI Converter\\Settings", true);

            if (LoudMaxCheck.Checked)
            {
                Settings.SetValue("loudmaxenabled", "1", Microsoft.Win32.RegistryValueKind.DWord);
                MainWindow.KMCGlobals.IsLoudMaxEnabled = true;
                LoudMaxCheck.ForeColor = Color.Green;
                LoudMaxCheck.Text = "LoudMax on";
            }
            else
            {
                Settings.SetValue("loudmaxenabled", "0", Microsoft.Win32.RegistryValueKind.DWord);
                MainWindow.KMCGlobals.IsLoudMaxEnabled = false;
                LoudMaxCheck.ForeColor = Color.DarkRed;
                LoudMaxCheck.Text = "LoudMax off";
            }

           Settings.Close();
        }
    }
}
