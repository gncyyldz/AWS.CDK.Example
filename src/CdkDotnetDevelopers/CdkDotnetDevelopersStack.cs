using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.S3;
using Constructs;

namespace CdkDotnetDevelopers
{
    public class CdkDotnetDevelopersStack : Stack
    {
        internal CdkDotnetDevelopersStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var logGroup = new LogGroup(this, "cdkLogs", new LogGroupProps()
            {
                 
            });


            #region Bucket - S3
            var s3Bucket = new Bucket(this, "example-bucket", new BucketProps
            {
                Versioned = true,
                AutoDeleteObjects = true,
                RemovalPolicy = RemovalPolicy.DESTROY
            });

            new CfnOutput(this, "BucketArn", new CfnOutputProps { Value = s3Bucket.BucketArn });
            #endregion

            #region Table - DynamoDB
            var dynamoDBTable = new Table(this, "example-table", new TableProps
            {
                TableName = "example-table",
                PartitionKey = new Attribute { Name = "id", Type = AttributeType.STRING },
                BillingMode = BillingMode.PAY_PER_REQUEST
            });

            new CfnOutput(this, "TableArn", new CfnOutputProps { Value = dynamoDBTable.TableArn });
            #endregion

            #region Lambda
            var lambdaFunction = new Function(this, "example-function", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                Code = Code.FromAsset("./src/CdkDotnetDevelopers/releases/example-lambda.zip"),
                Handler = "Example.Lambda.Project::Example.Lambda.Project.Function::FunctionHandler"
            });
            #endregion
        }
    }
}