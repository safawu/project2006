using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using System.IO;
using project2006.IO;
using Un4seen.Bass.AddOn.Fx;
using System.Runtime.InteropServices;

namespace project2006.Audio
{
    internal class AudioManager
    {
        public AudioManager()
        {
            initBass();
            ResetVolume();
            effectDict = new Dictionary<string, int>();
        }

        #region 主音乐

        private int audioHandle;
        private int audioStreamPrefilter;

        internal void LoadMainAudio(string name)
        {
            ReleaseMainAudio();
            audioFileStream = RWExternal.GetFileStream(name);
            if (audioFileStream == null)
            {
                throw new Exception("audio file not loaded!");
            }
            BASSFlag flags = BASSFlag.BASS_STREAM_DECODE;
            audioStreamPrefilter = Bass.BASS_StreamCreateFileUser(BASSStreamSystem.STREAMFILE_NOBUFFER, flags, procs, IntPtr.Zero);

            audioHandle = BassFx.BASS_FX_TempoCreate(audioStreamPrefilter, BASSFlag.BASS_DEFAULT);

            Bass.BASS_ChannelSetAttribute(audioHandle, BASSAttribute.BASS_ATTRIB_TEMPO_OPTION_USE_QUICKALGO, Bass.TRUE);
            Bass.BASS_ChannelSetAttribute(audioHandle, BASSAttribute.BASS_ATTRIB_TEMPO_OPTION_OVERLAP_MS, 4);
            Bass.BASS_ChannelSetAttribute(audioHandle, BASSAttribute.BASS_ATTRIB_TEMPO_OPTION_SEQUENCE_MS, 30);
        }

        internal void PlayMainAudio()
        {
            Bass.BASS_ChannelPlay(audioHandle, true);
        }

        internal void StopMainAudio()
        {
            Bass.BASS_ChannelSetPosition(audioHandle, 0);
            Bass.BASS_ChannelStop(audioHandle);
        }

        internal void TogglePauseMainAudio(bool pause)
        {
            if (pause && BASSActive.BASS_ACTIVE_PLAYING == Bass.BASS_ChannelIsActive(audioHandle))
            {
                Bass.BASS_ChannelPause(audioHandle);
            }
            else if (!pause)
            {
                Bass.BASS_ChannelPlay(audioHandle, false);
            }
        }
        /// <summary>
        /// 释放主音乐
        /// </summary>
        internal void ReleaseMainAudio()
        {
            if (audioHandle != 0)
            {
                Bass.BASS_ChannelStop(audioHandle);
                Bass.BASS_StreamFree(audioHandle);
                Bass.BASS_StreamFree(audioStreamPrefilter);
                audioHandle = 0;
                audioStreamPrefilter = 0;
            }

            if (audioFileStream != null)
            {
                audioFileStream.Dispose();
                audioFileStream = null;
            }
        }
        #endregion

        #region 效果音

        private Dictionary<string, int> effectDict;

        /// <summary>
        /// 读取效果文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cache">是否进字典缓存</param>
        /// <param name="loop">是否是循环音效</param>
        /// <returns>-1表示失败</returns>
        internal int loadEffect(string name, bool cache = true, bool loop = false)
        {
            BASSFlag flags = BASSFlag.BASS_SAMPLE_OVER_POS;
            if (loop)
            {
                flags |= BASSFlag.BASS_SAMPLE_LOOP;
            }
            int addr = -1;
            if (effectDict.TryGetValue(name, out addr))
            {
                return addr;
            }
            byte[] data = RWExternal.GetFileByte(name);
            if (data == null)
            {
                return -1;
            }
            addr = Bass.BASS_SampleLoad(data, 0, data.Length, 6, flags);
            if (cache)
            {
                effectDict.Add(name, addr);
            }
            return addr;
        }

        /// <summary>
        /// 播放效果音，前提是该音效已load
        /// </summary>
        /// <param name="name"></param>
        /// <param name="volumnMultipiler">音量缩放</param>
        internal void PlayEffect(string name, float volumnMultipiler = 1)
        {
            if (volumnMultipiler < 0)
            {
                volumnMultipiler = 0;
            }
            int addr = -1;
            if (effectDict.TryGetValue(name, out addr) && addr != -1)
            {
                int chan = Bass.BASS_SampleGetChannel(addr, false);
                Bass.BASS_ChannelSetAttribute(chan, BASSAttribute.BASS_ATTRIB_VOL,Math.Min((float)effectVolume / 100 * volumnMultipiler,1));
                Bass.BASS_ChannelPlay(chan, false);
            }
        }

        /// <summary>
        /// 清空效果音字典，释放句柄
        /// </summary>
        internal void ReleaseAllEffect()
        {
            foreach (int e in effectDict.Values)
                Bass.BASS_StreamFree(e);
            effectDict.Clear();
        }
        #endregion

        #region 音量相关

        private int mainVolume;
        private int effectVolume;

        internal void ResetVolume()
        {
            SetMainVolume(100);
            SetEffectVolume(100);
        }

        internal void SetMainVolume(int volume)
        {
            mainVolume = Math.Max(0, Math.Min(volume, 100));
            Bass.BASS_ChannelSetAttribute(audioHandle, BASSAttribute.BASS_ATTRIB_VOL, (float)mainVolume / 100);
        }

        internal void SetEffectVolume(int volume)
        {
            effectVolume = Math.Max(0, Math.Min(volume, 100));

        }
        #endregion
        #region  audio文件操纵
        static Stream audioFileStream;

        static void ac_Close(IntPtr user)
        {
            audioFileStream.Close();
        }

        static long ac_Length(IntPtr user)
        {
            return audioFileStream.Length;
        }

        static byte[] ac_ReadBuffer = new byte[32768];
        static int ac_Read(IntPtr buffer, int length, IntPtr user)
        {
            if (length > ac_ReadBuffer.Length)
                ac_ReadBuffer = new byte[length];

            if (!audioFileStream.CanRead)
                return 0;

            int readBytes = audioFileStream.Read(ac_ReadBuffer, 0, length);
            Marshal.Copy(ac_ReadBuffer, 0, buffer, length);
            return readBytes;
        }
        static bool ac_Seek(long offset, IntPtr user)
        {
            return audioFileStream.Seek(offset, SeekOrigin.Begin) == offset;
        }

        private BASS_FILEPROCS procs = new BASS_FILEPROCS(ac_Close, ac_Length, ac_Read, ac_Seek);
        #endregion
        private void initBass()
        {
            BassNet.Registration("poo@poo.com", "2X25242411252422");

            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, Game1.WindowHandle, null);

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, 100);
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_NET_BUFFER, 500);
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 10);
        }

    }
}
