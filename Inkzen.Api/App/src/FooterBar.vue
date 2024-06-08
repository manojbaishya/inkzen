<template>
    <IContainer fluid>
        <IRow class="row-vertical-spacing">
            <IColumn xs="9">
                <h1 class="font-weight_light d1 footer-margin-left">Inkzen Studios</h1>
            </IColumn>
            <IColumn xs="3">
                <div class="footer-margin-right">
                    <tr class="footer-align_right">
                        <td class="d1">■</td>
                    </tr>
                </div>
            </IColumn>
        </IRow>
        <IRow class="row-vertical-spacing">
            <IColumn xs="9">
                <img id="footer-section-arrow" class="footer-margin-left" src="/assets/Footer-Section-Arrow.svg"
                    alt="Footer-Section-Arrow">
                <div class="footer-margin-left">
                    <tr>
                        <td class="font-weight_light h4">
                            <a href="https://www.instagram.com/inkzenstudios">
                                Instagram
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td class="font-weight_light h4">
                            <a href="https://www.behance.net/theinkmonk">
                                Behance
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td class="font-weight_light h4">© 2024 by Inkzen Studios</td>
                    </tr>
                </div>
            </IColumn>
            <IColumn class="vertical-axis-align-bottom footer-align_right" xs="3">
                <div class="footer-margin-right">
                    <tr>
                        <td class="font-weight_light h4 footer-text-align_right">Christian Basti</td>
                    </tr>
                    <tr>
                        <td class="font-weight_light h4 footer-text-align_right">GHY 781005</td>
                    </tr>
                </div>
            </IColumn>
        </IRow>
    </IContainer>
</template>

<style>
#footer-section-arrow {
    width: 100px;
    filter: invert(100%);
    margin-bottom: calc(var(--scale-ratio-golden) * 1rem);
}

.row-vertical-spacing {
    margin-top: 10rem;
    margin-bottom: 10rem;
}

.footer-margin-left {
    margin-left: 3.618rem;
}


.footer-align_right {
    display: flex;
    justify-content: flex-end;
}

.footer-text-align_right {
    text-align: right;
}

.footer-margin-right {
    margin-right: 5.618em;
}


.footer-bottom-right {
    text-align: right;
    margin-right: calc(var(--scale-ratio-golden) * 1.9rem);
}

.vertical-axis-align-bottom {
    display: flex;
    align-items: flex-end;
}
</style>