using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Attributes
{
    public class AttachmentAttribute : Attribute
    {
        public AttachmentAttribute(object attachment) => Attachment = attachment;

        public object Attachment { get; private set; }
    }
}
