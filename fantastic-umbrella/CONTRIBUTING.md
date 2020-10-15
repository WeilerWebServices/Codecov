# Contributing to Codecov

:+1::tada: First off, thanks for taking the time to contribute! :tada::+1:

The following is a set of guidelines for contributing to Codecov and its packages, which are hosted in the Codecov Organization on Github.  These are mostly guidelines, not rules. Use your best judgment, and feel free to propose changes to this document in a pull request.

> Note: Please don't file an issue to ask a question. You'll get faster results by using the resources below.

Please check our [FAQ](https://docs.codecov.io/docs/frequently-asked-questions) and other [documentation](https://docs.codecov.io/docs/) first to see if your question has already been answered.  

If you still can't find a solution to your problem, shoot us an email at support@codecov.io

## How Can I Contribute?

This section guides you through submitting a bug report for Codecov. Following these guidelines helps maintainers and the community understand your report :pencil:, reproduce the behavior :computer: :computer:, and find related reports :mag_right:.

> Note: If you find a Closed issue that seems like it is the same thing that you're experiencing, open a new issue and include a link to the original issue in the body of your new one.

### Before Submitting A Bug Report

* **Check the [FAQ](https://docs.codecov.io/docs/frequently-asked-questions)** for a list of common questions and problems.
* **Perform a [cursory search](https://github.com/search?q=+is%3Aissue+user%3Acodecov&state=open)** to see if the problem has already been reported. If it has **and the issue is still open**, add a comment to the existing issue instead of opening a new one.

### Submitting A (Good) Bug Report

Create an issue on the respective repository and provide the following information by filling in [the template](ISSUE_TEMPLATE.md).

Explain the problem and include additional details to help maintainers reproduce the problem:

* **Use a clear and descriptive title** for the issue to identify the problem.
* **Describe the exact steps which reproduce the problem** in as many details as possible.  When listing steps, **don't just say what you did, but explain how you did it**.
* **Provide specific examples to demonstrate the steps**. Include links to files or GitHub projects, or copy/pasteable snippets, which you use in those examples.
* **Describe the behavior you observed after following the steps** and point out what exactly is the problem with that behavior.
* **Explain which behavior you expected to see instead and why.**
* **Include screenshots and animated GIFs** which show you following the described steps and clearly demonstrate the problem. You can use [this tool](https://www.cockos.com/licecap/) to record GIFs on macOS and Windows, and [this tool](https://github.com/colinkeenan/silentcast) or [this tool](https://github.com/GNOME/byzanz) on Linux.

Provide more context by answering these questions:

* **Did the problem start happening recently** or was this always a problem?
* **Can you reliably reproduce the issue?** If not, provide details about how often the problem happens and under which conditions it normally happens.
* If the problem is related to working with files (e.g. opening and editing files), **does the problem happen for all files and projects or only some?** Does the problem happen only when working with large files? Is there anything else special about the files you are using?

Include details about your configuration and environment:

* **Which version of Codecov are you using?** 
* **What's the name and version of the OS you're using**?

### Submitting A (Good) Pull Request

The process described here has several goals:

- Maintain Codecov's quality
- Fix problems that are important to users
- Engage the community in working toward the best possible Codecov
- Enable a sustainable system for Codecov's maintainers to review contributions

Please follow these steps to have your contribution considered by the maintainers:

1. Follow all instructions in [the template](PULL_REQUEST_TEMPLATE.md)
2. After you submit your pull request, verify that all [status checks](https://help.github.com/articles/about-status-checks/) are passing <details><summary>What if the status checks are failing?</summary>If a status check is failing, and you believe that the failure is unrelated to your change, please leave a comment on the pull request explaining why you believe the failure is unrelated. A maintainer will re-run the status check for you. If we conclude that the failure was a false positive, then we will open an issue to track that problem with our status check suite.</details>

While the prerequisites above must be satisfied prior to having your pull request reviewed, the reviewer(s) may ask you to complete additional design work, tests, or other changes before your pull request can be ultimately accepted.

Thanks to the team at [Atom](https://github.com/atom/atom/blob/master/CONTRIBUTING.md) for providing an awesome template for this document.
