﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using umbraco;

namespace InfoCaster.Umbraco.UrlTracker
{
	public static class UrlTrackerSettings
	{
		public const string TableName = "icUrlTracker";
		public const string OldTableName = "infocaster301";
		public const string HttpModuleCheck = "890B748D-E226-49FA-A0D7-9AFD3A359F88";

		/// <summary>
		/// Returns wether or not the UrlTracker is disabled
		/// </summary>
		/// <remarks>
		/// appSetting: 'urlTracker:disabled'
		/// </remarks>
		public static bool IsDisabled
		{
			get
			{
				if (!_isDisabled.HasValue)
				{
					bool isDisabled = false;
					if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["urlTracker:disabled"]))
					{
						bool parsedAppSetting;
						if (bool.TryParse(ConfigurationManager.AppSettings["urlTracker:disabled"], out parsedAppSetting))
							isDisabled = parsedAppSetting;
					}
					_isDisabled = isDisabled;
				}
				return _isDisabled.Value;
			}
		}
		/// <summary>
		/// Returns wether or not logging for the UrlTracker is enabled
		/// </summary>
		/// <remarks>
		/// appSettings: 'urlTracker:enableLogging' & 'umbracoDebugMode'
		/// </remarks>
		public static bool EnableLogging
		{
			get
			{
				if (!_enableLogging.HasValue)
				{
					bool enableLogging = false;
					if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["urlTracker:enableLogging"]))
					{
						bool parsedAppSetting;
						if (bool.TryParse(ConfigurationManager.AppSettings["urlTracker:enableLogging"], out parsedAppSetting))
							enableLogging = parsedAppSetting;
					}
					_enableLogging = enableLogging;
				}
				return _enableLogging.Value && GlobalSettings.DebugMode;
			}
		}
		/// <summary>
		/// Returns the URLs to ignore for 404 Not Found logging
		/// </summary>
		/// <remarks>
		/// appSetting: 'urlTracker:404UrlsToIgnore'
		/// </remarks>
		public static string[] NotFoundUrlsToIgnore
		{
			get
			{
				if (_notFoundUrlsToIgnore == null)
				{
					_notFoundUrlsToIgnore = new string[] { "favicon.ico" };
					if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["urlTracker:404UrlsToIgnore"]))
						_notFoundUrlsToIgnore = _notFoundUrlsToIgnore.Union(ConfigurationManager.AppSettings["urlTracker:404UrlsToIgnore"].Split(',')).ToArray();
				}
				return _notFoundUrlsToIgnore;
			}
		}
		/// <summary>
		/// Returns wether or not tracking URL changes is disabled
		/// </summary>
		/// <remarks>
		/// appSetting: 'urlTracker:trackingDisabled'
		/// </remarks>
		public static bool IsTrackingDisabled
		{
			get
			{
				if (!_isTrackingDisabled.HasValue)
				{
					bool isTrackingDisabled = false;
					if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["urlTracker:trackingDisabled"]))
					{
						bool parsedAppSetting;
						if (bool.TryParse(ConfigurationManager.AppSettings["urlTracker:trackingDisabled"], out parsedAppSetting))
							isTrackingDisabled = parsedAppSetting;
					}
					_isTrackingDisabled = isTrackingDisabled;
				}
				return _isTrackingDisabled.Value;
			}
		}

		static bool? _isDisabled;
		static bool? _enableLogging;
		static string[] _notFoundUrlsToIgnore;
		static bool? _isTrackingDisabled;
	}
}