﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina'</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace RawCMS.Library.Lambdas
{
    public abstract class RestLambda : HttpLambda
    {
        public HttpContext Request;

        public override object Execute(HttpContext request)
        {
            Request = request;
            using (StreamReader reader = new StreamReader(request.Request.Body))
            {
                string body = reader.ReadToEnd();
                JObject input = new JObject();
                try
                {
                    input = JObject.Parse(body);
                }
                catch (Exception)
                {
                    //TODO: add log here
                }

                return Rest(input);
            }
        }

        public abstract JObject Rest(JObject input);
    }
}