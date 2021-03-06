﻿#region License

// Copyright (c) 2005-2014, CellAO Team
// 
// 
// All rights reserved.
// 
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
// 
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//     * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// 
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
// 

#endregion

namespace ZoneEngine.Core.MessageHandlers
{
    #region Usings ...

    using CellAO.Core.Components;
    using CellAO.Core.Entities;

    using SmokeLounge.AOtomation.Messaging.Messages.N3Messages;

    using ZoneEngine.Core.InternalMessages;

    #endregion

    /// <summary>
    /// </summary>
    [MessageHandler(MessageHandlerDirection.OutboundOnly)]
    public class ChatTextMessageHandler : BaseMessageHandler<ChatTextMessage, ChatTextMessageHandler>
    {
        #region Outbound

        /// <summary>
        /// </summary>
        /// <param name="character">
        /// </param>
        /// <param name="text">
        /// </param>
        /// <param name="unknown1">
        /// </param>
        /// <param name="unknown2">
        /// </param>
        /// <param name="unknown3">
        /// </param>
        public void Send(ICharacter character, string text, byte unknown1 = 0, byte unknown2 = 0, int unknown3 = 0)
        {
            this.Send(character, Filler(character, text, unknown1, unknown2, unknown3));
        }

        /// <summary>
        /// </summary>
        /// <param name="character">
        /// </param>
        /// <param name="text">
        /// </param>
        /// <param name="unknown1">
        /// </param>
        /// <param name="unknown2">
        /// </param>
        /// <param name="unknown3">
        /// </param>
        /// <returns>
        /// </returns>
        private static MessageDataFiller Filler(
            ICharacter character,
            string text,
            byte unknown1 = 0,
            byte unknown2 = 0,
            int unknown3 = 0)
        {
            return x =>
            {
                x.Identity = character.Identity;
                x.Text = text;
                x.Unknown1 = unknown1;
                x.Unknown2 = unknown2;
                x.Unknown3 = unknown3;
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="character">
        /// </param>
        /// <param name="text">
        /// </param>
        /// <param name="unknown1">
        /// </param>
        /// <param name="unknown2">
        /// </param>
        /// <param name="unknown3">
        /// </param>
        /// <returns>
        /// </returns>
        public ChatTextMessage Create(
            ICharacter character,
            string text,
            byte unknown1 = 0,
            byte unknown2 = 0,
            int unknown3 = 0)
        {
            return this.Create(character, Filler(character, text, unknown1, unknown2, unknown3));
        }

        /// <summary>
        /// </summary>
        /// <param name="character">
        /// </param>
        /// <param name="text">
        /// </param>
        /// <param name="unknown1">
        /// </param>
        /// <param name="unknown2">
        /// </param>
        /// <param name="unknown3">
        /// </param>
        /// <returns>
        /// </returns>
        public IMSendAOtomationMessageBodyToClient CreateIM(
            ICharacter character,
            string text,
            byte unknown1 = 0,
            byte unknown2 = 0,
            int unknown3 = 0)
        {
            return new IMSendAOtomationMessageBodyToClient()
                   {
                       Body =
                           this.Create(
                               character,
                               Filler(
                                   character,
                                   text.Replace("<", "&lt;")
                           .Replace(">", "&gt;"),
                                   unknown1,
                                   unknown2,
                                   unknown3)),
                       client = character.Controller.Client
                   };
        }

        #endregion
    }
}