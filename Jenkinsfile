pipeline {
    agent any

    environment {
        // Thiết lập thông tin AWS từ Jenkins credentials
        AWS_DEFAULT_REGION = 'ap-northeast-1'
        AWS_ACCESS_KEY_ID = credentials('aws-access-key-id')
        AWS_SECRET_ACCESS_KEY = credentials('aws-secret-access-key')
    }

    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/sannhtpd07870/FreshX-BE.git'
            }
        }
        
        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }
        
        stage('Build') {
            steps {
                sh 'dotnet publish -c Release -o dev'
            }
        }

        stage('Archive') {
            steps {
                sh 'zip -r myapp.zip dev/'
            }
        }

        stage('Deploy') {
            steps {
                withAWS(credentials: 'aws-credentials-id', region: 'ap-northeast-1') {
                    awsCodeDeploy applicationName: 'FreshX',
                                  deploymentGroupName: 'FreshXAPI',
                                  s3bucket: 'arn:aws:s3:::freshxbucket',
                                  s3key: 'myapp.zip',
                                  region: 'ap-northeast-1'
                }
            }
        }
    }

    post {
        success {
            echo 'Deployment successfully finished.'
        }
        failure {
            echo 'Deployment failed.'
        }
    }
}
