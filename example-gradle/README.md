# [Codecov](https://codecov.io) Gradle Example
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fcodecov%2Fexample-gradle.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2Fcodecov%2Fexample-gradle?ref=badge_shield)


## Guide
### Travis Setup

Add to your `.travis.yml` file.
```yml
language: java
jdk:
  - oraclejdk8
before_script:
  - chmod +x gradlew
script:
  - ./gradlew check
  - ./gradlew codeCoverageReport
after_success:
  - bash <(curl -s https://codecov.io/bash)
```

### Produce Coverage Reports
1. Add Jacoco Plugin to your `build.gradle`. [See here](https://github.com/codecov/example-gradle/blob/master/build.gradle#L5)
2. Set Jacoco to export xml. [See here](https://github.com/codecov/example-gradle/blob/master/build.gradle#L18-L23)
3. Execute your tests as normal
4. Call `gradle jacocoTestReport` to generate report. [See here](https://github.com/codecov/example-gradle/blob/65f88382659cf17c8693c3079941a12c8d004f03/circle.yml#L3)

### FAQ
- Q: Do you support Multi-module projects?<br/>A:Update your parent (root) `build.gradle`:
```groovy
allprojects {
    apply plugin: 'java'
    apply plugin: 'maven'
    apply plugin: 'jacoco'

    sourceCompatibility = 1.8
    targetCompatibility = 1.8

    repositories {
        mavenLocal()
        mavenCentral()
        jcenter()

        maven { url "http://repo1.maven.org/maven2/" }
    }
}

subprojects {
    dependencies {
        ...
    }

    test.useTestNG()
}

task codeCoverageReport(type: JacocoReport) {
    executionData fileTree(project.rootDir.absolutePath).include("**/build/jacoco/*.exec")

    subprojects.each {
        sourceSets it.sourceSets.main
    }

    reports {
        xml.enabled true
        xml.destination file("${buildDir}/reports/jacoco/report.xml")
        html.enabled false
        csv.enabled false
    }
}

codeCoverageReport.dependsOn {
    subprojects*.test
}
```

## Caveats
### Private Repo
Repository tokens are required for (a) all private repos, (b) public repos not using Travis-CI, CircleCI or AppVeyor. Find your repository token at Codecov and provide via appending `-t <your upload token>` to you where you upload reports.

## Links
- [Community Boards](https://community.codecov.io)
- [Support](https://codecov.io/support)
- [Documentation](https://docs.codecov.io)


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fcodecov%2Fexample-gradle.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2Fcodecov%2Fexample-gradle?ref=badge_large)