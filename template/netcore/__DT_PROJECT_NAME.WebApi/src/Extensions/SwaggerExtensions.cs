using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace __DT_PROJECT_NAME.WebApi.Extensions {
    public static class SwaggerExtensions {
        public static void OperationFileResultFilter(this SwaggerGenOptions options) => options.OperationFilter<FileFilter>();
    }

    class FileFilter : IOperationFilter {

        public void Apply(Operation operation, OperationFilterContext context) {
            if (context.MethodInfo.ReturnType == typeof(FileResult)) {
                operation.Produces = new[] { "application/octet-stream" };
            }
        }
    }
}
