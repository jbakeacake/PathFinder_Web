pipeline {
  agent {
    docker {
      args '-d -p 3000:3000 '
      image 'node:14-alpine'
    }

  }
  stages {
    stage('Fetch Dependencies') {
      steps {
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