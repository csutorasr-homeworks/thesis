using MediatR;
using NJsonSchema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flottapp.WebApi
{
    internal class SchemaNameGenerator : DefaultSchemaNameGenerator, ISchemaNameGenerator
    {
        public override string Generate(Type type)
        {
            var originalType = type;
            if (type.IsNested)
            {
                type = type.DeclaringType;
            }
            if (IsCommand(type))
            {
                return GetDtoNameFromCommand(type);
            }
            return base.Generate(originalType);
        }

        private bool IsCommand(Type type)
        {
            if (type.IsEnum)
            {
                return false;
            }
            while (type != null)
            {
                if (type.GetInterfaces().Any(x => x == typeof(IRequest) || x.GetGenericTypeDefinition() == typeof(IRequest<>)))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

        private string GetDtoNameFromCommand(Type type)
        {
            var name = type.Name;
            if (name.EndsWith("Command"))
            {
                name = name.Substring(0, name.Length - "Command".Length);
            }
            return name + "Dto";
        }
    }
}
