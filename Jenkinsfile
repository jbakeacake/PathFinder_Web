pipeline {
  agent any
  stages {
    stage('Fetch Dependencies') {
      steps {
        nodejs('NodeJS-Default') {
          echo 'Finished Configuring NodeJS'
        }

        sh '''cd ./client-app/
npm install'''
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