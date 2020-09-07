pipeline {
  agent any
  stages {
    stage('Fetch Dependencies') {
      steps {
        sh '''cd ./client-app
npm install'''
      }
    }

    stage('Unit Testing') {
      steps {
        echo 'Fetch Dependencies Complete. Begin Unit Testing...'
      }
    }

  }
}