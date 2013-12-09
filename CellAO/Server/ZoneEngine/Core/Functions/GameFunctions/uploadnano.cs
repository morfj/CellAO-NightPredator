﻿#region License

// Copyright (c) 2005-2013, CellAO Team
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//     * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
// EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

namespace ZoneEngine.Core.Functions.GameFunctions
{
    #region Usings ...

    using CellAO.Core.Entities;
    using CellAO.Core.Nanos;
    using CellAO.Database.Dao;
    using CellAO.Enums;

    using MsgPack;

    using SmokeLounge.AOtomation.Messaging.GameData;
    using SmokeLounge.AOtomation.Messaging.Messages.N3Messages;

    #endregion

    /// <summary>
    /// </summary>
    public class uploadnano : FunctionPrototype
    {
        #region Fields

        /// <summary>
        /// </summary>
        private FunctionType functionId = FunctionType.UploadNano;

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public override FunctionType FunctionId
        {
            get
            {
                return this.functionId;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="self">
        /// </param>
        /// <param name="caller">
        /// </param>
        /// <param name="target">
        /// </param>
        /// <param name="arguments">
        /// </param>
        /// <returns>
        /// </returns>
        public override bool Execute(
            INamedEntity self, 
            INamedEntity caller, 
            IInstancedEntity target, 
            MessagePackObject[] arguments)
        {
            var temp = new UploadedNano() { NanoId = arguments[0].AsInt32() };
            ((Character)self).UploadedNanos.Add(temp);
            UploadedNanosDao.WriteNano(((Character)self).Identity.Instance, temp);

            var message = new CharacterActionMessage()
                          {
                              Identity = self.Identity,
                              Action = CharacterActionType.UploadNano,
                              Target = self.Identity,
                              Parameter1 = (int)IdentityType.NanoProgram,
                              Parameter2 = temp.NanoId,
                              Unknown = 0
                          };
            ((Character)self).Client.SendCompressed(message);


            return true;
        }

        #endregion
    }
}