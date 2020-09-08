pipeline {
  agent any
  stages {
    stage('Fetch Dependencies') {
      steps {
        nodejs('Node') {
          echo 'Finished Configuring NodeJS'
          sh '''echo "Node Version:"
node -v
npm install npm -g
echo "npm Version:"
npm -v
npm install'''
        }

      }
    }

    stage('Unit Testing') {
      steps {
        echo 'Begin Unit Testing...'
      }
    }

    stage('Build / Compose') {
      steps {
        sh 'docker-compose build'
      }
    }

    stage('Push') {
      steps {
        sh 'docker-compose push'
      }
    }

    stage('Success') {
      steps {
        echo 'Build and Deployment Success!'
      }
    }

  }
}