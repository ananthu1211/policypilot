# What I Learned This Week: Azure CLI Automation, MFA Authentication, & Resource Management

## 1. Resource Groups, Naming Conventions, & Governance

* **Resource Group Purpose:** Serves as a logical container in Azure to manage, deploy, and delete related assets together (e.g., VMs, databases, container registries). Creating a Resource Group itself costs **₹0 / $0**.
* **Standard Naming Convention (`rg-`):** Following Microsoft's Cloud Adoption Framework (CAF), resource groups use the `rg-` prefix (e.g., `rg-policypilot-dev`). Standard structure follows:

$$\text{\{prefix\}-\{app-name\}-\{environment\}-\{region\}}$$


* **Resource Groups vs. Subscriptions for Environment Isolation:**
* **Resource Groups (Single Subscription):** Best for developers and smaller projects. Combines billing under one invoice, simple to manage, and allows resource isolation.
* **Separate Subscriptions (Multi-Subscription):** Used by enterprises for strict blast-radius isolation, distinct departmental invoicing, independent resource quotas, and absolute Role-Based Access Control (RBAC) security boundaries.



## 2. Portal vs. CLI Dynamics & Hybrid Workflow

* **Azure Portal (Web GUI):** Ideal for visual exploration, reviewing feature sets, and checking real-time pricing tiers.
* **Azure CLI (Command Line):** Built for speed, repeatable automation (CI/CD integration), and bulk cost management.
* **The Recommended Hybrid Workflow:**
1. **Explore in Portal:** Inspect pricing tiers (e.g., Free vs. Basic B1) and configuration options visually.
2. **Execute via CLI:** Provision resources quickly via one-line terminal commands.
3. **Teardown via CLI:** Wipe out an entire test environment in one step with `az group delete --name <rg-name> --yes` to stop billing immediately.



## 3. Azure CLI Authentication & Troubleshooting

* **Terminal PATH Persistence:** After installing the Azure CLI via `winget` or MSI, running terminal sessions will not recognize the `az` command until the terminal is restarted or the `$env:Path` environment variable is explicitly reloaded.
* **Multi-Factor Authentication (MFA / Error `AADSTS50076`):** Standard `az login` can fail if security defaults or MFA are enforced on the Microsoft Entra ID (Azure AD) tenant.
* **Fix 1 (Explicit Tenant):** `az login --tenant <tenant-id>` forces direct interactive MFA browser redirect.
* **Fix 2 (Device Code):** `az login --use-device-code` provides an explicit 8-character code via `[https://microsoft.com/devicelogin](https://microsoft.com/devicelogin)` to safely bypass browser session redirects.


* **Credential Cleanup:** `az logout` revokes active tokens; `az account clear` purges all cached subscription data from the local machine (`~/.azure`).

## 4. Steps & Commands Executed

1. **Installed Azure CLI:**
```powershell
winget install -e --id Microsoft.AzureCLI

```


2. **Reloaded PATH in Active PowerShell Session (without restarting terminal):**
```powershell
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")

```


3. **Authenticated via Explicit Tenant ID (Bypassed MFA Error `AADSTS50076`):**
```powershell
az login --tenant bfe70287-d3a6-49bf-af8f-4e491d18fd63

```


4. **Verified Active Account & Subscription:**
```powershell
az account show --output table

```


5. **Created the Development Resource Group:**
```powershell
az group create --name rg-policypilot-dev --location centralindia

```


6. **Verified Resource Group Creation:**
```powershell
az group list --output table

```


7. **Logged Out & Cleared Credentials:**
```powershell
az logout
az account clear

```



## 5. Key Questions & Doubts Clarified

* **"If we use CLI instead of Portal, won't we miss pricing tier details and make expensive mistakes?"**
* *Answer:* CLI commands force you to declare SKUs/tiers explicitly, or you can query available tiers using commands like `az vm list-sizes`. Combining Portal exploration with CLI creation gives full cost transparency.


* **"Can't we separate Dev and Prod using different Resource Groups instead of separate Subscriptions?"**
* *Answer:* Yes! For single-developer projects like `PolicyPilot`, separate Resource Groups (`rg-policypilot-dev` and `rg-policypilot-prod`) inside `subscription1` are completely sufficient. Enterprises use separate subscriptions primarily to avoid shared quota limits and cross-environment permission leaks.


* **"What does `rg` at the start of `rg-policypilot-dev` stand for?"**
* *Answer:* It stands for **Resource Group**. Standard Azure CAF prefixes (e.g., `rg-`, `vm-`, `app-`, `kv-`) make resources easily identifiable in logs and bills.


* **"How do I logout or terminate the Azure CLI connection in PowerShell?"**
* *Answer:* Run `az logout` to invalidate active tokens, or `az account clear` to erase all local cached profile data. Running `az account show` afterward confirms complete disconnection.


# What I Learned This Week: Enterprise Resource Group Governance & Security Controls

## 1. Governance & Financial Tracking (Resource Tags)

* **Purpose & Mechanism:** Tags are metadata Key-Value pairs attached to Azure resources (e.g., `Environment=Dev`, `CostCenter=CC-104`). They do not change runtime behavior, but act as database indexes for financial tracking, searching, and filtering.
* **Cost Center Concept:** An accounting label representing a specific department or project that incurs expenses. Filtering the master Azure bill by `CostCenter` allows finance teams to route exact cloud charges to internal departmental budgets.
* **Cost Analysis Integration:** Inside **Cost Management + Billing $\rightarrow$ Cost analysis**, you can use **Group by Tag $\rightarrow$ CostCenter** or **Filter by Tag $\rightarrow$ Environment** to isolate spending across teams, projects, or environments.
* **Execution Options:**
* **Azure CLI:**
```powershell
az group create --name rg-policypilot-dev --location centralindia --tags Environment=Dev CostCenter=CC-104

```


* **Azure Portal:**
Open `rg-policypilot-dev` $\rightarrow$ Click **Tags** in the left menu (or next to Overview) $\rightarrow$ Enter Key & Value pairs $\rightarrow$ Save.



---

## 2. Accidental Protection (Resource Locks)

* **Purpose & Mechanism:** Resource Locks act as speed bumps to prevent accidental deletion or configuration changes. Unlike permissions, locks are applied **directly to the resource container**, not to user email addresses, and affect all users globally.
* **Lock Types:**
* **`CanNotDelete` (Delete Lock):** Users can read and modify resources, but cannot delete the Resource Group or its child assets.
* **`ReadOnly`:** Users can only view configurations; they cannot add, edit, or delete anything.


* **Lock Inheritance & Deletion Workflow:**
* **Inheritance:** A lock applied at the Resource Group scope cascades down to **every nested resource** (VMs, Databases, App Services) inside it.
* **Deletion Process:** You cannot run `az group delete` while a lock exists. To delete a locked Resource Group, you must first **remove the lock**, and then execute the deletion command.


* **Execution Options:**
* **Azure CLI:**
```powershell
# 1. Apply CanNotDelete lock
az lock create --name PreventAccidentalDelete --lock-type CanNotDelete --resource-group rg-policypilot-dev

# 2. List active locks
az lock list --resource-group rg-policypilot-dev --output table

# 3. Delete lock before wiping group
az lock delete --name PreventAccidentalDelete --resource-group rg-policypilot-dev

```


* **Azure Portal:**
Open `rg-policypilot-dev` $\rightarrow$ Settings $\rightarrow$ **Locks** $\rightarrow$ Click **+ Add** (or click the trash icon to delete an existing lock).



---

## 3. Access Control (Scope-Based RBAC)

* **Purpose & Mechanism:** Role-Based Access Control (RBAC) grants specific permissions to individual user email addresses or group security principals at a specific scope (e.g., Resource Group level).
* **Key Core Roles:**
* **`Reader`:** View-only access; cannot create or modify resources.
* **`Contributor`:** Standard developer role; can **create, edit, and delete any resource** in the Resource Group, but **cannot grant permissions** to others.
* **`Owner`:** Full administrative control + ability to assign/revoke RBAC roles and manage locks.


* **Execution Options:**
* **Azure CLI:**
```powershell
az role assignment create --assignee dev-user@company.com --role "Contributor" --resource-group rg-policypilot-dev

```


* **Azure Portal:**
Open `rg-policypilot-dev` $\rightarrow$ **Access control (IAM)** $\rightarrow$ **+ Add** $\rightarrow$ **Add role assignment** $\rightarrow$ Select `Contributor` $\rightarrow$ Select user email $\rightarrow$ **Review + assign**.



---

## 4. Resource Group Architecture (Shared Lifecycle Rule)

* **The Golden Rule:** All resources placed within a single Resource Group should share the **exact same lifecycle** (deployed together, updated together, and destroyed together).
* **Good Practice:** Grouping an App Service, its dev Database, and its Key Vault inside `rg-policypilot-dev` so they can be managed or torn down safely without affecting other applications.
* **Bad Practice:** Placing central corporate networking, shared VPN gateways, or shared enterprise databases in an application-specific Resource Group.

---

## 5. Key Doubts & Questions Clarified

* **"Are tags just text labels or do they filter things?"**
* *Answer:* They are text labels that Azure natively integrates into Cost Management, search bars, CLI queries, and policy compliance tools to slice, group, and filter data dynamically.


* **"What is a Cost Center?"**
* *Answer:* An accounting code representing an internal department or project budget. Tagging resources with a Cost Center allows central IT to route exact Azure bill totals back to the responsible business unit.


* **"Do I add an email address when creating a Resource Lock?"**
* *Answer:* No. Resource Locks apply directly to the Resource Group object and block everyone globally. Email addresses are attached when assigning **RBAC Roles** via Access Control (IAM).


* **"If I place a lock on a Resource Group, are the VMs and Databases inside protected too?"**
* *Answer:* Yes. Locks follow strict parent-to-child inheritance across all nested resources.


* **"If a resource group has a Delete Lock, can I delete it?"**
* *Answer:* Not directly. You must first remove the lock (requires `Owner` permissions), and then run the delete command.


* **"Which role should I give a developer who needs to add resources in the future?"**
* *Answer:* Assign the **Contributor** role inside the Resource Group's Access Control (IAM) settings.


