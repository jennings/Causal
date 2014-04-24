Implementation Notes
=====================

Causal Updater
---------------

Definitions:

* _Updater_: The project built by the Causal.Updater project.
* _Target product_: The software that is automatically updated by
  the Updater.
* _Registration_: The target product must register with the Updater
  in order to be updated.

Runs on the same machine as the target product.

Responsible for:

* Checking for updates from the Internet
* Downloading available updates
* Listening for the target product to initiate the update

The target product must always call into the Updater before an update
begins.

Endpoints:

* http://localhost/causalupdater/1/update/{productId}

  * `GET`: Returns information about any pending update.
  * `POST`: Initiates an update, if available.

* http://localhost/causalupdater/1/registration/{productId}

  * `GET`: Gets information about a registered software product.
  * `POST`: Registers a software product.
  * `DELETE`: De-registers a software product.