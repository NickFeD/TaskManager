import {
    DrawerBody,
    DrawerHeader,
    DrawerHeaderTitle,
    InlineDrawer,
    Button
} from "@fluentui/react-components";
import { Dismiss24Regular } from "@fluentui/react-icons";
import { useState } from "react";

function MainPage() {
    const [isOpen, setIsOpen] = useState(false);

    return (
        <div>
            <InlineDrawer
                position="start"
                open={isOpen}
                onOpenChange={(_, { open }) => setIsOpen(open)}
            >
                <DrawerHeader>
                    <DrawerHeaderTitle
                        action={
                            <Button
                                appearance="subtle"
                                aria-label="Close"
                                icon={<Dismiss24Regular />}
                                onClick={() => setIsOpen(false)}
                            />
                        }
                    >
                        Overlay Drawer
                    </DrawerHeaderTitle>
                </DrawerHeader>

                <DrawerBody>
                    <p>Drawer content</p>
                </DrawerBody>
            </InlineDrawer>

            <div>
                <Button appearance="primary" onClick={() => setIsOpen(true)}>
                    Open Drawer
                </Button>
            </div>
        </div>
    )
}
export default MainPage;