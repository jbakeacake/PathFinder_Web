pipeline {
  agent any
  stages {
    stage('Fetch Dependencies') {
      steps {
        sh '''ls
docker images'''
      }
    }

    stage('Unit Testing') {
      steps {
        echo 'Fetch Dependencies Complete. Begin Unit Testing...'
      }
    }

  }
}