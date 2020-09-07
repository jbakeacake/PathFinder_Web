pipeline {
  agent {
    docker {
      image 'node:14.9.0-stretch-slim'
      args '-p 3000:3000'
    }

  }
  stages {
    stage('Fetch Dependencies') {
      steps {
        sh '''cd ./client-app
ls
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