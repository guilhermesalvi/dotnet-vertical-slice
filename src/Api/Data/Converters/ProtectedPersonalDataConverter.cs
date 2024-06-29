using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VerticalSlice.Api.Data.Security;

namespace VerticalSlice.Api.Data.Converters;

public class ProtectedPersonalDataConverter(DataSecurityService dataSecurityService)
    : ValueConverter<string, string>(
        v => dataSecurityService.Encrypt(v),
        v => dataSecurityService.Decrypt(v));
