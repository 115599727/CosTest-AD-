using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace VstStream
{
    class VstIniParser
    {
        private String iniFilePath;

        private List<Section> SectionList;

        private class Section
        {
            internal String SectionName;
            internal List<KeyValue> KeyValueList;
        }

        private class KeyValue
        {
            internal string Key;
            internal string Value;
        }

        /// <summary>
        /// Opens the INI file at the given path and enumerates the values in the IniParser.
        /// </summary>
        /// <param name="iniPath">Full path to INI file.</param>
        internal VstIniParser(String iniPath)
        {
            VstStreamReader iniFile = null;
            String strLine = null;
            Section currentSection = null;
            string strSection = null;
            this.SectionList = new List<Section>();

            iniFilePath = iniPath;

            if (File.Exists(iniPath))
            {
                try
                {
                    iniFile = new VstStreamReader(iniPath);

                    strLine = iniFile.ReadLine();

                    while (strLine != null)
                    {
                        //strLine = strLine.Trim().ToUpper();
                        strLine = strLine.Trim();

                        if (strLine != "")
                        {
                            if (strLine.StartsWith("'"))
                            {
                                // Do Nothing
                            }
                            else if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                            {
                                strSection = strLine.Substring(1, strLine.Length - 2);
                                currentSection = new Section();
                                currentSection.SectionName = strSection;
                                currentSection.KeyValueList = new List<KeyValue>();
                                this.SectionList.Add(currentSection);
                            }
                            else
                            {
                                if (currentSection == null)
                                {
                                    strLine = iniFile.ReadLine();
                                    continue;
                                }

                                string[] keySplit = strLine.Split(new char[] { '=' }, 2);

                                if (keySplit.Length > 1 && this.KeyExisted(currentSection, keySplit[0]) == false)
                                {
                                    KeyValue keyVal = new KeyValue();
                                    keyVal.Key = keySplit[0].Trim();
                                    keyVal.Value = keySplit[1].Trim();
                                    currentSection.KeyValueList.Add(keyVal);
                                }
                            }
                        }

                        strLine = iniFile.ReadLine();
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    if (iniFile != null)
                        iniFile.Close();
                }
            }
            else
                throw new FileNotFoundException("Unable to locate " + iniPath);
        }

        /// <summary>
        /// Return if the Section Name Existed
        /// </summary>
        /// <param name="sectionName">Section Name</param>
        /// <returns></returns>
        private bool SectionExisted(string sectionName)
        {
            for (int i = 0; i < this.SectionList.Count; i++)
            {
                if (this.SectionList[i].SectionName == sectionName)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Return if the Key Name Existed
        /// </summary>
        /// <param name="section">Section</param>
        /// <param name="keyName">Key Name</param>
        /// <returns></returns>
        private bool KeyExisted(Section section, string keyName)
        {
            if (section.KeyValueList == null || section.KeyValueList.Count == 0)
                return false;

            for (int i = 0; i < section.KeyValueList.Count; i++)
            {
                if (section.KeyValueList[i].Key == keyName)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the value for the given section, key pair.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="settingName">Key name.</param>
        internal String GetSetting(String sectionName, String settingName, int sectionIndex = 0)
        {
            int index = -1;
            foreach (Section sect in this.SectionList)
            {
                if (sect.SectionName == sectionName)
                {
                    index++;
                    if (index != sectionIndex)
                        continue;

                    foreach (KeyValue keyVal in sect.KeyValueList)
                    {
                        if (keyVal.Key == settingName)
                            return keyVal.Value;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// Enumerates all lines for given section.
        /// </summary>
        /// <param name="sectionName">Section to enum.</param>
        internal String[] EnumSection(String sectionName)
        {
            ArrayList tmpArray = new ArrayList();

            foreach (Section sect in this.SectionList)
            {
                if (sect.SectionName == sectionName)
                {
                    foreach (KeyValue keyVal in sect.KeyValueList)
                    {
                        tmpArray.Add(keyVal.Key);
                    }
                }
            }

            return (String[])tmpArray.ToArray(typeof(String));
        }

        /// <summary>
        /// Enumerates all lines for given section.
        /// </summary>
        internal String[] EnumSectionNames()
        {
            ArrayList tmpArray = new ArrayList();

            foreach (Section sect in this.SectionList)
            {
                tmpArray.Add(sect.SectionName);
            }

            return (String[])tmpArray.ToArray(typeof(String));
        }

        /// <summary>
        /// Adds or replaces a setting to the table to be saved.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        /// <param name="settingValue">Value of key.</param>
        internal void AddSetting(String sectionName, String settingName, String settingValue, int sectionIndex = 0)
        {
            // 检查Section是否存在
            bool sectionExisted = false;
            int i=0, index = -1;
            for(;i<this.SectionList.Count;i++)
            {
                if (this.SectionList[i].SectionName == sectionName)
                {
                    index++;
                    if (index == sectionIndex)
                    {
                        sectionExisted = true;
                        break;
                    }
                }
            }

            // Section已经存在
            if(sectionExisted)
            {
                // 更新对应的KeyValue
                for (int j = 0; j < this.SectionList[i].KeyValueList.Count; j++)
                {
                    if (this.SectionList[i].KeyValueList[j].Key == settingName)
                    {
                        this.SectionList[i].KeyValueList[j].Value = settingValue;
                        return;
                    }
                }
                // 如果KeyValue不存在，添加一个
                KeyValue keyVal = new KeyValue();
                keyVal.Key = settingName;
                keyVal.Value = settingValue;
                this.SectionList[i].KeyValueList.Add(keyVal);
            }
            // Section不存在，添加Section和KeyValue
            else
            {
                Section sect = new Section();
                sect.SectionName = sectionName;
                sect.KeyValueList = new List<KeyValue>();
                KeyValue keyVal = new KeyValue();
                keyVal.Key = settingName;
                keyVal.Value = settingValue;
                sect.KeyValueList.Add(keyVal);
                this.SectionList.Add(sect);
            }
        }

        /// <summary>
        /// Adds or replaces a setting to the table to be saved with a null value.
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        internal void AddSetting(String sectionName, String settingName, int sectionIndex = 0)
        {
            AddSetting(sectionName, settingName, "", sectionIndex);
        }

        /// <summary>
        /// Remove a setting.
        /// </summary>
        /// <param name="sectionName">Section to delete under.</param>
        /// <param name="settingName">Key name to delete.</param>
        internal void DeleteSetting(String sectionName, String settingName)
        {
            foreach (Section sect in this.SectionList)
            {
                if (sect.SectionName == sectionName)
                {
                    foreach (KeyValue keyVal in sect.KeyValueList)
                    {
                        if (keyVal.Key == settingName)
                            sect.KeyValueList.Remove(keyVal);
                    }
                }
            }
        }

        /// <summary>
        /// Remove the whole section
        /// </summary>
        /// <param name="sectionName">Section to delete</param>
        internal void DeleteSetting(String sectionName)
        {
            for(int i = this.SectionList.Count - 1; i >= 0; i--)
            {
                if (this.SectionList[i].SectionName == sectionName)
                {
                    this.SectionList.RemoveAt(i);
                }                    
            }
        }

        /// <summary>
        /// Raname the whole section
        /// </summary>
        /// <param name="sectionName">Old Section Name</param>
        /// <param name="newSectionName">New Section Name</param>
        internal void RenameSetting(String sectionName, String newSectionName)
        {
            for (int i = 0; i < this.SectionList.Count; i++)
            {
                if (this.SectionList[i].SectionName == sectionName)
                {
                    Section sect = this.SectionList[i];
                    sect.SectionName = newSectionName;
                    this.SectionList[i] = sect;
                    break;
                }
            }
        }

        /// <summary>
        /// Save settings to new file.
        /// </summary>
        /// <param name="newFilePath">New file path.</param>
        internal void SaveSettings(String newFilePath)
        {
            String strToSave = "";

            foreach (Section sect in this.SectionList)
            {
                strToSave += ("[" + sect.SectionName + "]\r\n");

                foreach (KeyValue keyVal in sect.KeyValueList)
                {
                    strToSave += (keyVal.Key + " = " + keyVal.Value + "\r\n");
                }

                strToSave += ("\r\n");
            }

            try
            {
                VstStreamWriter tw = new VstStreamWriter(newFilePath);
                tw.Write(strToSave);
                tw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Save settings back to ini file.
        /// </summary>
        internal void SaveSettings()
        {
            SaveSettings(iniFilePath);
        }
    }
}
