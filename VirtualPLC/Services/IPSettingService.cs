﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualPLC.Interfaces;

namespace VirtualPLC.Services
{
    public class IPSettingService : IConfigManager
    {
        #region FIELDS
        private readonly string _configDirectory;
        public event Action ConfigRead;
        #endregion

        #region PROPERTIES
        public string IP { get; set; }
        public int Port { get; set; }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// SECS/GEM 통신을 위한 IP 설정 서비스 레이어
        /// </summary>
        /// <param name="configDirectory"></param>
        public IPSettingService(string configDirectory)
        {
            _configDirectory = configDirectory;
            if (!Directory.Exists(_configDirectory))
                Directory.CreateDirectory(_configDirectory);

            InitConfig();
        }
        #endregion

        #region COMMAND
        #endregion

        #region METHOD

        /// <summary>
        /// 파일 파싱
        /// </summary>
        public void InitConfig()
        {
            string filePath = GetFilePathAndCreateIfNotExists();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                // 각 줄에서 '=' 또는 ':' 기준으로 키와 값을 분리
                if (line.StartsWith("IP"))
                {
                    IP = line.Split('=')[1].Trim();
                }
                else if (line.StartsWith("Port"))
                {
                    if (int.TryParse(line.Split('=')[1].Trim(), out int portValue))
                        Port = portValue;
                }
            }
            ConfigRead?.Invoke();
        }

        /// <summary>
        /// IP 업데이트 메서드
        /// </summary>
        public void UpdateConfigValue(string key, string newValue)
        {
            string filePath = GetFilePathAndCreateIfNotExists();
            // 파일의 모든 줄을 읽어옵니다.
            var lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++)
            {
                // 각 줄이 해당 key로 시작하는지 확인 (예: "IP", "Port")
                if (lines[i].StartsWith(key))
                {
                    // 구분자(: 또는 =)에 따라 새로운 값으로 줄을 만듭니다.
                    if (lines[i].Contains("="))
                        lines[i] = $"{key} = {newValue}";
                    else if (lines[i].Contains(":"))
                        lines[i] = $"{key} : {newValue}";
                }
            }

            // 수정된 내용을 파일에 다시 씁니다.
            File.WriteAllLines(filePath, lines);
        }

        /// <summary>
        /// Config 파일 경로 반환 없을 경우 생성
        /// </summary>
        public string GetFilePathAndCreateIfNotExists()
        {
            string fileName = $"PLC_IP.config";
            string filePath = Path.Combine(_configDirectory, fileName);

            // 파일이 없으면 생성하면서 내용도 쓴다
            if (!File.Exists(filePath))
            {
                string content = "IP = 127.0.0.1\nPort = 502";
                File.WriteAllText(filePath, content);
            }

            return filePath;
        }
        #endregion
    }
}
