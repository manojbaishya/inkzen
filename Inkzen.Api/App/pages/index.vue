<template>
    <div>
        <video
            id="homepage-video"
            autoplay
            loop
            muted
        >
            <source
                src="~/assets/Home/loop.mp4"
                type="video/mp4"
            >
        </video>

        <img
            id="hero-image"
            class="_margin-y:4!"
            src="/hero-image.jpg"
        >
    </div>
</template>

<style scoped>
#homepage-video {
    min-width: 100%;
    max-width: 100%;
    object-fit: cover;
}

#hero-image {
    min-width: 100%;
    max-width: 100%;
    object-fit: cover;
}
</style>