// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="JsonImageListSource.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XLabs.Serialization;

namespace XLabs.Samples.Data
{
    public class JsonImageListSource : IImageListSource
    {
        private readonly IJsonSerializer serializer;

        public JsonImageListSource(IJsonSerializer serializer)
        {
            this.serializer = serializer;
        }
        public async Task<List<ImageListItem>> GetListItems()
        {
            var assembly = typeof(JsonImageListSource).GetTypeInfo().Assembly;
            using (var reader = 
                new StreamReader(assembly.GetManifestResourceStream("XLabs.Sample.Data.PresidentList.json"))
                )
            {
                var str = await reader.ReadToEndAsync();
                return this.serializer.Deserialize<List<ImageListItem>>(str);
            }
        }
    }
}