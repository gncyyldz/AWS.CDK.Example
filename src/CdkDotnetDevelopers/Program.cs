using Amazon.CDK;

namespace CdkDotnetDevelopers
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new CdkDotnetDevelopersStack(app, "CdkDotnetDevelopersStack", new StackProps
            {

            });
            app.Synth();
        }
    }
}
