/* * *******************************************************************************************
*  This file is part of the Oracle Service Cloud Accelerator Reference Integration set published
 *  by Oracle Service Cloud under the Universal Permissive License (UPL), Version 1.0
 *  included in the original distribution.
 *  Copyright (c) 2014, 2015, 2016, Oracle and/or its affiliates. All rights reserved.
 ***********************************************************************************************
 *  Accelerator Package: OSvC + OFSC Reference Integration
 *  link: http://www-content.oracle.com/technetwork/indexes/samplecode/accelerator-osvc-2525361.html
 *  OSvC release: 15.2 (Feb 2015)
 *  OFSC release: 15.2 (Feb 2015)
 *  reference: 150622-000130
 *  date: Thu Sep  3 23:14:00 PDT 2015

 *  revision: rnw-15-11-fixes-release-03
*  SHA1: $Id: 3b693601df2174561d0a4f2d409b9cb6fcc65cbc $
* *********************************************************************************************
*  File: ToaUserUtil.cs
* ****************************************************************************************** */

using System;
using System.Configuration;
using Oracle.RightNow.Toa.Client.ActivityProxyService;
using Oracle.RightNow.Toa.Client.InboundProxyService;
using Oracle.RightNow.Toa.Client.Rightnow;


namespace Oracle.RightNow.Toa.Client.Common
{
    /// <summary>
    /// A static class for 'user' 
    /// This class will be visible within Toa client only.
    /// </summary>
    internal static class ToaUserUtil
    {

        //Note: 
        //This boolean flag needs to be turned off/false for Oracle Field Service Cloud 19B and below to disable SHA256 hasing and use MD5 hashing instead.
        //For OFSC 19C and above this flag needs to be true to enable SHA256 hashing.
        static Boolean isSHA256Enabled = true;

        /// <summary>
        /// Get instance of 'user' class used in Activity service
        /// </summary>
        /// <returns></returns>
        public static user GetActivityUser()
        {
            user userObj = new user();
            string now = DateTime.Now.ToString("O");

            userObj.login = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.UserName);
            userObj.company = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.CompanyName);
            var password = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.Password);

            userObj.now = now;
            if(isSHA256Enabled){
                userObj.auth_string = ToaHashUtil.SHA256AuthString(now, userObj.login, password);
            }else{
                userObj.auth_string = ToaHashUtil.MD5AuthString(now, password);
            }
            

            return userObj;
        }


        /// <summary>
        /// Get instance of 'UserElement' class used in Inbound service
        /// </summary>
        /// <returns></returns>
        public static UserElement GetInboundUser()
        {
            var userObj = new UserElement();
            string now = DateTime.Now.ToString("O");

            userObj.login = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.UserName);
            userObj.company = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.CompanyName);
            var password = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.Password);
            userObj.now = now;

            if (isSHA256Enabled)
            {
                userObj.auth_string = ToaHashUtil.SHA256AuthString(now, userObj.login, password);
            }
            else
            {
                userObj.auth_string = ToaHashUtil.MD5AuthString(now, password);
            }

            return userObj;
        }

        /// <summary>
        /// Get instance of 'user_element' class used in Capacity service
        /// </summary>
        /// <returns></returns>
        public static user_element GetCapacityUser()
        {
            var userObj = new user_element();
            string now = DateTime.Now.ToString("O");

            userObj.login = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.UserName);
            userObj.company = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.CompanyName);
            var password = RightNowConfigService.GetConfigValue(RightNowConfigKeyNames.Password);
            userObj.now = now;

            if (isSHA256Enabled)
            {
                userObj.auth_string = ToaHashUtil.SHA256AuthString(now, userObj.login, password);
            }
            else
            {
                userObj.auth_string = ToaHashUtil.MD5AuthString(now, password);
            }

            return userObj;
        }
    }
}
