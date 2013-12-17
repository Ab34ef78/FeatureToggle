﻿// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NFeature.
// 
// NFeature is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NFeature is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NFeature.  If not, see <http://www.gnu.org/licenses/>.

namespace NFeature.Configuration
{
	using System;
	using System.Configuration;

	public static class ConfigurationManager<T> where T : ConfigurationSectionBase, new()
	{
		public static T Section(Func<T> onMissingSection = null) {
			var config = new T();
			var section = Section(config.SectionName);

			if (section == null) {
				return new T().OnMissingConfiguration() as T;
			}

			return section;
		}

		public static T Section(string sectionName) {
			return (T) ConfigurationManager.GetSection(sectionName);
		}

        public static T Section(string sectionName, string XMLconfigFilename)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = XMLconfigFilename;
            
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            try
            {
                return config.Sections[sectionName] as T;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public static Configuration GetXMLConfig(string XMLconfigFilename)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = XMLconfigFilename; 
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            return config;

        }
	}
}