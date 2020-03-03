using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using StrongHeart.Features.Core;
using StrongHeart.Features.Decorators.RequestValidation;

namespace StrongHeart.Features.Decorators.Audit
{
    public class CreateFeatureAuditDto
    {
        private CreateFeatureAuditDto(Guid featureId, ICaller caller, string requestArgumentsJson, string? responseArgumentsJson, TimeSpan duration, FeatureAuditStatus status, Guid? correlationKey, bool? isOnBehalfOfOther)
        {
            FeatureId = featureId;
            Caller = caller;
            RequestArgumentsJson = requestArgumentsJson;
            ResponseArgumentsJson = responseArgumentsJson;
            Duration = duration;
            Status = status;
            CorrelationKey = correlationKey;
            IsOnBehalfOfOther = isOnBehalfOfOther;
            CreatedDate = DateTime.Now; //TODO: use UTC or take a dependency for getting the timestamp
        }

        public Guid FeatureId { get; }
        public ICaller Caller { get; }
        public string RequestArgumentsJson { get; }
        public string? ResponseArgumentsJson { get; }
        public TimeSpan Duration { get; }
        public FeatureAuditStatus Status { get; }
        public DateTime CreatedDate { get; }
        public Guid? CorrelationKey { get; }

        /// <summary>
        /// If request has failed we don't know for sure is the value has been properly calculated. Therefore null
        /// </summary>
        public bool? IsOnBehalfOfOther { get; }

        public static CreateFeatureAuditDto CreateSuccessWithResponse<TRequest, TResponse>(Guid featureId, TRequest request, TResponse response, TimeSpan duration, Guid? correlationKey, bool isOnBehalfOfOther)
            where TRequest : IRequest
        {
            string requestArguments = ToJson(request);
            string? responseArguments = null;
            if (response != null)
            {
                dynamic re = response;
                responseArguments = ToJson(re.Value);
            }

            return new CreateFeatureAuditDto(featureId, request.Caller, requestArguments, responseArguments, duration, FeatureAuditStatus.Success, correlationKey, isOnBehalfOfOther);
        }

        public static CreateFeatureAuditDto CreateSuccessWithNoResponse<TRequest, TResponse>(Guid featureId, TRequest request, TResponse response, TimeSpan duration, Guid? correlationKey, bool isOnBehalfOfOther)
            where TRequest : IRequest
        {
            return CreateSuccessWithResponse(featureId, request, null as string, duration, correlationKey, isOnBehalfOfOther);
        }

        public static CreateFeatureAuditDto CreateResultFailure<TRequest>(Guid featureId, TRequest request, string error, TimeSpan duration, Guid? correlationKey)
            where TRequest : IRequest
        {
            string requestArguments = ToJson(request);
            string responseArguments = ToJson(new {ResultError = error});
            FeatureAuditStatus status = FeatureAuditStatus.ResultFailure;

            return new CreateFeatureAuditDto(featureId, request.Caller, requestArguments, responseArguments, duration, status, correlationKey, null /*if request has failed we dont know for sure is the value has been properly calculated. Therefore null.*/);
        }

        public static CreateFeatureAuditDto CreateException<TRequest>(Guid featureId, TRequest request, Exception ex, TimeSpan duration, Guid? correlationKey)
            where TRequest : IRequest
        {
            string requestArguments = ToJson(request);

            FeatureAuditStatus status = FeatureAuditStatus.OtherException;
            if (ex.GetBaseException() is BusinessValidationException)
            {
                status = FeatureAuditStatus.ValidationException;
            }
            else if (ex.GetBaseException() is BusinessException)
            {
                status = FeatureAuditStatus.BusinessException;
            }
            return new CreateFeatureAuditDto(featureId, request.Caller, requestArguments, ToJson(ex), duration, status, correlationKey, null /*if request has failed we dont know for sure is the value has been properly calculated. Therefore null.*/);
        }

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            IgnoreNullValues = false,
            WriteIndented = false,
            Converters =
            {
                new OmitByteArrayConverter(),
                new OmitCallerConverter(),
            }
        };

        private static string ToJson(object input)
        {
            return JsonSerializer.Serialize(input, Options);
        }

        private class OmitCallerConverter : JsonConverter<ICaller>
        {
            public override ICaller Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotSupportedException();
            }

            public override void Write(Utf8JsonWriter writer, ICaller value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.Id);
            }
        }

        private class OmitByteArrayConverter : JsonConverter<byte[]>
        {
            public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotSupportedException();
            }

            public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
            {
                writer.WriteStringValue("omitted");
            }
        }
    }
}