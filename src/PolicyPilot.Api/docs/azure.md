# What I Learned This Week: Azure Fundamentals, Billing, & CLI Basics

## 1. Azure Hierarchy, Subscriptions & Resource Groups

* **Azure Subscription:** An administrative and billing container that connects cloud resources to a payment method (e.g., Pay-As-You-Go). Every deployed resource must belong to a subscription to track charges.
* **Resource Group:** A logical container inside a subscription that holds related resources for an application or project (e.g., VMs, databases, virtual networks).
* **The Azure Hierarchy:**
* **Billing Account:** Top level (linked to your credit card/billing profile).
* **Subscription:** Billing container (e.g., `subscription1`).
* **Resource Group:** Project folder (e.g., `rg-learning`).
* **Resources:** Individual assets (e.g., Virtual Machines, Databases, Public IPs).


* **Multiple Subscriptions in Enterprises:** Organizations split subscriptions to isolate **Dev/Test vs. Production** environments, enforce granular Role-Based Access Control (RBAC), track departmental costs, and bypass single-subscription resource quotas.

## 2. Cost Management, Budgets & Safety Controls

* **Budget Functionality:** Azure budgets act as early-warning security nets via email alerts rather than instant automated hard kill-switches.
* **Reporting Latency:** Usage data takes **8 to 24 hours** to process and reflect in Cost Management.
* **Budget Scopes:**
* **Subscription-Level Budget:** Monitors spending only within a specific subscription container.
* **Billing Account-Level Budget:** Master budget tracking total spending across all subscriptions and services.


* **Cost Safety Best Practices:**
* Group test resources into a single Resource Group so everything can be deleted at once to stop charges.
* Enable **Auto-shutdown** schedules on Virtual Machines during creation.
* Monitor accrued costs regularly via **Cost Management + Billing $\rightarrow$ Cost analysis**.



## 3. Azure CLI vs. Azure Portal

* **Azure Portal (Web GUI):** Graphical web interface best suited for initial learning, visual architecture inspection, and one-off administrative tasks.
* **Azure CLI (Command Line):** Cross-platform command-line tool used to create, manage, and delete Azure resources using text commands in Terminal or PowerShell.
* **CLI Advantages:** Enables rapid deployment, script automation, and instant teardown of entire resource groups without navigating multiple web pages.

## 4. Steps Executed

1. **Configured Master Budget:** Created an initial budget alert under **Cost Management + Billing** with email notifications.
2. **Upgraded Account:** Converted account profile to Pay-As-You-Go status.
3. **Activated Subscription:** Confirmed active Pay-As-You-Go subscription container named `subscription1`.
4. **Installed Azure CLI:** Set up the Azure CLI locally on the development machine.
5. **Authenticated CLI Session:** Executed `az login` to connect the local terminal to the active Azure account via browser authentication.

## 5. Key Questions & Doubts Clarified

* **"If cost data has an 8–24 hour latency, how does a budget help?"**
* *Answer:* It prevents forgotten running resources from accumulating charges over an entire month by triggering automated alerts (Actual or Forecasted) early in the billing cycle.


* **"Where do I view my current charges and past monthly bills?"**
* *Answer:* Live unbilled spending is under **Cost Management $\rightarrow$ Cost analysis**. Finalized monthly statements are under **Billing $\rightarrow$ Invoices**.


* **"Why did Azure ask me to create a subscription after I upgraded my account?"**
* *Answer:* Account conversion updates payment profile details, but resources require an explicit **Subscription container** to attach to for billing and governance.


* **"Is `subscription1` a valid Pay-As-You-Go subscription?"**
* *Answer:* Yes. `subscription1` is the default name assigned by Azure when an active Pay-As-You-Go subscription container is generated.


* **"Why are there budgets in both the subscription tab and Cost Management + Billing?"**
* *Answer:* The subscription budget tracks only that individual container, whereas the Cost Management budget acts as a master alert covering the entire billing account.


* **"How does deleting a Resource Group stop billing?"**
* *Answer:* Azure cascades the deletion command to all nested assets (disks, IPs, network interfaces, VMs), permanently removing them and ending all associated hourly charges.