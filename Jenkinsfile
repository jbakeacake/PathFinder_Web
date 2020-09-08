pipeline {
  agent any
  stages {
    stage('Fetch Dependencies') {
      steps {
        sh 'echo Fetch Deps'
      }
    }

    stage('Unit Testing') {
      steps {
        echo 'Fetch Dependencies Complete. Begin Unit Testing...'
      }
    }

    stage('Build / Compose') {
      steps {
        sh 'docker --version'
      }
    }

    stage('Push') {
      steps {
        sh '/usr/local/bin/docker-compose push'
      }
    }

    stage('Success') {
      steps {
        echo 'Build and Deployment Success!'
      }
    }

  }
}