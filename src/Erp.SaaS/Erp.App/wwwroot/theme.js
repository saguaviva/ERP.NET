(function () {
    const validThemes = new Set(["current", "blue-modern", "compact", "ultra-compact"]);
    const collapsedNavSectionsStorageKey = "erp-collapsed-nav-sections-v2";

    function normalize(theme) {
        return validThemes.has(theme) ? theme : "current";
    }

    function apply(theme) {
        const resolved = normalize(theme);
        document.documentElement.setAttribute("data-theme", resolved);
        return resolved;
    }

    function readStoredTheme() {
        try {
            return normalize(window.localStorage.getItem("erp-theme") || "current");
        } catch {
            return "current";
        }
    }

    window.erpTheme = {
        get() {
            const resolved = apply(readStoredTheme());
            return resolved;
        },
        apply,
        set(theme) {
            const resolved = apply(theme);

            try {
                window.localStorage.setItem("erp-theme", resolved);
            } catch {
            }

            return resolved;
        }
    };

    function applySidebarHidden(hidden) {
        const resolved = hidden === true;
        document.documentElement.setAttribute("data-sidebar-hidden", resolved ? "true" : "false");
        return resolved;
    }

    function normalizeCollapsedSections(value) {
        if (!Array.isArray(value)) {
            return [];
        }

        return value.filter(item => typeof item === "string" && item.length > 0);
    }

    window.erpUi = {
        getSidebarHidden() {
            try {
                return applySidebarHidden(window.localStorage.getItem("erp-sidebar-hidden") === "true");
            } catch {
                return applySidebarHidden(false);
            }
        },
        setSidebarHidden(hidden) {
            const resolved = applySidebarHidden(hidden);

            try {
                window.localStorage.setItem("erp-sidebar-hidden", resolved ? "true" : "false");
            } catch {
            }

            return resolved;
        },
        getCollapsedNavSections() {
            try {
                const raw = window.localStorage.getItem(collapsedNavSectionsStorageKey);
                if (raw === null) {
                    return null;
                }

                return normalizeCollapsedSections(JSON.parse(raw));
            } catch {
                return null;
            }
        },
        setCollapsedNavSections(keys) {
            const resolved = normalizeCollapsedSections(keys);

            try {
                window.localStorage.setItem(collapsedNavSectionsStorageKey, JSON.stringify(resolved));
            } catch {
            }

            return resolved;
        }
    };
})();
